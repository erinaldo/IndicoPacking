Use [IndicoPacking]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPC_SynchroniseOrders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SPC_SynchroniseOrders]
GO

-- =============================================
-- Author:	Siwanka De Silva	
-- Create date: 31st August 2015
-- Description:	This SP will has functionlaity to Synchonise Order Deatils
-- =============================================
CREATE PROCEDURE [dbo].[SPC_SynchroniseOrders] 
	@WeekNo int,
	@WeekEndDate datetime2(7)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Get the shipment Id	
	DECLARE @ShipmentId int

	IF(NOT EXISTS(	SELECT ID
					FROM [IndicoPacking].[dbo].[Shipment] s
					WHERE s.WeekNo = @WeekNo 
							AND s.WeekendDate = @WeekEndDate))
	BEGIN
		INSERT INTO [dbo].[Shipment]
           ([WeekNo]
           ,[WeekendDate]
           ,[IndicoWeeklyProductionCapacityID])
		VALUES
           (@WeekNo,
           @WeekEndDate,
           (SELECT TOP 1 w.[ID] 
			FROM [Indico].[dbo].[WeeklyProductionCapacity] w
			WHERE w.WeekNo = @WeekNo 
					AND w.WeekendDate = @WeekEndDate))

		SET @ShipmentId = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SELECT @ShipmentId = ID
		FROM [IndicoPacking].[dbo].[Shipment] s
		WHERE s.WeekNo = @WeekNo 
				AND s.WeekendDate = @WeekEndDate 
	END

	-- ShipmentDeatil records
	INSERT INTO [IndicoPacking].[dbo].[ShipmentDetail]
				   ([Shipment]
				   ,[IndicoDistributorClientAddress]
				   ,[ShipTo]
				   ,[Port]
				   ,[ShipmentMode]
				   ,[PriceTerm]
				   ,[ETD]
				   ,[Qty]
				   ,[QuantityFilled]
				   ,[QuantityYetToBeFilled]
				   ,[InvoiceNo])
	SELECT	@ShipmentId,
			dca.ID,
			dca.CompanyName,
			dp.Name,
			sm.Name,
			pm.Name,
			@WeekEndDate,
			0,
			0,
			0,
			''
	FROM [Indico].[dbo].[OrderDetail] od
		JOIN [Indico].[dbo].[DistributorClientAddress] dca
			ON od.[DespatchTo] = dca.[ID]
		JOIN [Indico].[dbo].[DestinationPort] dp
			ON dca.[Port] = dp.[ID]
		JOIN [Indico].[dbo].[ShipmentMode] sm
			ON od.[ShipmentMode] = sm.[ID]	
		JOIN [Indico].[dbo].[PaymentMethod] pm
			ON od.[PaymentMethod] = pm.[ID]		
		LEFT OUTER JOIN [IndicoPacking].[dbo].[ShipmentDetail] sd
			ON dca.ID = sd.IndicoDistributorClientAddress
				AND sd.Shipment = @ShipmentId			
	WHERE (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, @WeekEndDate)) AND CAST(@WeekEndDate as DATE)) AND sd.ID IS NULL
	GROUP BY dca.ID,
			dca.CompanyName,
			dp.Name,
			sm.Name,
			pm.Name

	-- OrderDetailItem records
	SELECT	sd.ID,
			sd.IndicoDistributorClientAddress INTO #tempShipmentDeatils
	FROM [IndicoPacking].[dbo].[ShipmentDetail] sd
	WHERE sd.Shipment = @ShipmentId

	DECLARE @ShipmentDetailId int
	DECLARE @IndicoDistributorClientAddress int

	CREATE TABLE #tempOrderDetailBreakdown
	(
		OrderID int NOT NULL, 
		OrderDeatilID int NOT NULL, 
		SizeName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, 
		Total int NOT NULL,
		Qty int NOT NULL
	)

	ALTER TABLE #tempOrderDetailBreakdown ADD PRIMARY KEY NONCLUSTERED (OrderID, OrderDeatilID, SizeName, Qty)

	-- Get the breakdown of orderdetail items to recursive CTE
	SELECT  o.ID AS OrderID,
			od.ID AS OrderDeatilID,
			s.SizeName,
			odq.Qty INTO #tempOrderDeatilsWithQuantity
	FROM [Indico].[dbo].[OrderDetail] od
		INNER JOIN [Indico].[dbo].[OrderDetailQty] odq
			ON od.[ID] = odq.[OrderDetail]		
		INNER JOIN [Indico].[dbo].[Order] o
			ON od.[Order] = o.ID
		INNER JOIN [Indico].[dbo].[Size] s
			ON odq.[Size] = s.[ID]
	WHERE (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, @WeekEndDate)) AND CAST(@WeekEndDate as DATE)) AND odq.Qty != 0

	-- CTE
	;WITH OrderDetailItemsCTE (OrderID, OrderDeatilID, SizeName, Total, Qty) AS (
		SELECT OrderID, OrderDeatilID, SizeName, Qty, Qty - 1
		FROM #tempOrderDeatilsWithQuantity
		WHERE Qty > 1

		UNION ALL

		SELECT t.OrderID, t.OrderDeatilID, t.SizeName, t.Qty, cte.Qty - 1
		FROM #tempOrderDeatilsWithQuantity t
			INNER JOIN OrderDetailItemsCTE cte 
				ON cte.OrderID = t.OrderID
					AND cte.OrderDeatilID = t.OrderDeatilID
					AND cte.SizeName = t.SizeName
		WHERE cte.Qty > 1
	)

	-- Now we have the breakdown
	INSERT INTO #tempOrderDetailBreakdown (OrderID, OrderDeatilID, SizeName, Total, Qty)
	SELECT OrderID, OrderDeatilID, SizeName, Qty, Qty  
	FROM #tempOrderDeatilsWithQuantity 
	UNION ALL
	SELECT OrderID, OrderDeatilID, SizeName, Total, Qty 
	FROM OrderDetailItemsCTE 
	ORDER BY OrderID, OrderDeatilID, SizeName

	WHILE (EXISTS(SELECT TOP 1 ID FROM #tempShipmentDeatils))
	BEGIN
		-- Get the top 1 from shipment detail 
		SELECT TOP 1 @ShipmentDetailId = ID,
					@IndicoDistributorClientAddress = IndicoDistributorClientAddress
		FROM #tempShipmentDeatils

		-- Insert New ones
		INSERT INTO [IndicoPacking].[dbo].[OrderDeatilItem]
				   ([ShipmentDeatil]
				   ,[IndicoOrderID]
				   ,[IndicoOrderDetailID]
				   ,[ShipmentDetailCarton]
				   ,[OrderType]
				   ,[Distributor]
				   ,[Client]
				   ,[PurchaseOrder]
				   ,[VisualLayout]
				   ,[OrderNumber]
				   ,[Pattern]
				   ,[ItemSubGroup]
				   ,[SizeDesc]
				   ,[SizeQty]
				   ,[SizeSrno]
				   ,[Status]
				   ,[PrintedCount]
				   ,[PatternImage]
				   ,[VLImage]
				   ,[PatternNumber])	
		SELECT		@ShipmentDetailId AS ShipmentDetailId,
					o.ID AS OrderId,
					od.ID AS OrderDetailId,
					NULL AS ShipmentDetailCarton,
					ot.[Name] AS OrderType,
					dis.[Name] AS Distributor,
					c.[Name] AS Client,
					o.[PurchaseOrderNo],
					vl.[NamePrefix],
					o.[ID] AS PurchaseOrder,
					p.[NickName],
					COALESCE(i.[Name], '') AS ItemSubGroup,
					t.SizeName,
					t.Total AS Quantity,
					t.Qty AS QtySequence,
					os.Name AS [Status],
					0 AS PrintedCount,
					COALESCE(
						(SELECT TOP 1 
							'http://gw.indiman.net/IndicoData/PatternTemplates/' + CAST(pti.Pattern AS nvarchar(8)) + '/' + pti.[Filename] + pti.Extension
						FROM [Indico].[dbo].[PatternTemplateImage] pti WHERE p.ID = pti.Pattern AND pti.IsHero = 1
						), '' 
					) AS PatternImagePath,
					COALESCE(
						(SELECT TOP 1 
							'http://gw.indiman.net/IndicoData/VisualLayout/' + CAST(vl.ID AS nvarchar(8)) + '/' + im.[Filename] + im.Extension
						FROM [Indico].[dbo].[Image] im WHERE vl.ID = im.VisualLayout AND im.IsHero = 1
						), '' 
					) AS PatternImagePath,
					p.[Number]
		FROM [Indico].[dbo].[OrderDetail] od
			INNER JOIN #tempOrderDetailBreakdown t
				ON t.OrderDeatilID = od.ID
			LEFT OUTER JOIN [IndicoPacking].[dbo].[OrderDeatilItem] odi	
				ON t.OrderID = odi.IndicoOrderID
					AND t.OrderDeatilID = odi.IndicoOrderDetailID
					AND t.SizeName = odi.SizeDesc
					AND t.Qty = odi.SizeSrno
			INNER JOIN [Indico].[dbo].[Order] o
				ON od.[Order] = o.ID
			INNER JOIN [Indico].[dbo].[OrderStatus] os
				ON o.[Status] = os.ID
			INNER JOIN [Indico].[dbo].[Company] dis
				ON dis.[ID] = o.[Distributor]		
			INNER JOIN [Indico].[dbo].[OrderType] ot
				ON od.[OrderType] = ot.[ID]
			INNER JOIN [Indico].[dbo].[VisualLayout] vl
				ON od.[VisualLayout] = vl.ID	
			INNER JOIN [Indico].[dbo].Pattern p
				ON od.Pattern = p.ID
			LEFT OUTER JOIN [Indico].[dbo].[Item] i
				ON p.[SubItem] = i.ID
			INNER JOIN [Indico].[dbo].[Client] c
				ON o.Client = c.ID					
		WHERE odi.ID IS NULL AND od.DespatchTo = @IndicoDistributorClientAddress
		ORDER BY o.ID, odi.IndicoOrderDetailID, t.SizeName, t.Total, t.Qty

		-- Modify the OrderDetailItems

		
		-- Delete the OrderDetailsItems


		-- Delete the top record of shipment detail
		DELETE #tempShipmentDeatils
		FROM #tempShipmentDeatils
		WHERE ID = @ShipmentDetailId
				AND IndicoDistributorClientAddress = @IndicoDistributorClientAddress
	END

	IF OBJECT_ID('tempdb..#tempOrderDeatilsWithQuantity') IS NOT NULL 
		DROP Table #tempOrderDeatilsWithQuantity

	IF OBJECT_ID('tempdb..#tempOrderDetailBreakdown') IS NOT NULL 
		DROP Table #tempOrderDetailBreakdown

	IF OBJECT_ID('tempdb..#tempShipmentDeatils') IS NOT NULL 
		DROP Table #tempShipmentDeatils
END
