USE [IndicoPacking]
GO

/****** Object:  StoredProcedure [dbo].[SPC_SynchroniseOrders]    Script Date: 19/10/2015 11:18:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Siwanka De Silva	
-- Create date: 31st August 2015
-- Description:	This SP will has functionlaity to Synchonise Order Deatils
-- =============================================
ALTER PROCEDURE [dbo].[SPC_SynchroniseOrders] 
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
		OrderID int, 
		OrderDeatilID int, 
		SizeName nvarchar(255), 
		Qty int
	)
	
	WHILE (EXISTS(SELECT TOP 1 ID FROM #tempShipmentDeatils))
	BEGIN
		-- Get the top 1 from shipment detail 
		SELECT TOP 1 @ShipmentDetailId = ID,
					@IndicoDistributorClientAddress = IndicoDistributorClientAddress
		FROM #tempShipmentDeatils
		
		-- Get the breakdown of orderdetail items to recursion CTE
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
		;WITH OrderDetailItemsCTE (OrderID, OrderDeatilID, SizeName, Qty) AS (
			SELECT OrderID, OrderDeatilID, SizeName, Qty - 1
			FROM #tempOrderDeatilsWithQuantity
			WHERE Qty > 1

			UNION ALL

			SELECT t.OrderID, t.OrderDeatilID, t.SizeName, cte.Qty - 1
			FROM #tempOrderDeatilsWithQuantity t
				INNER JOIN OrderDetailItemsCTE cte 
					ON cte.OrderID = t.OrderID
						AND cte.OrderDeatilID = t.OrderDeatilID
						AND cte.SizeName = t.SizeName
			WHERE cte.Qty > 1
		)

		-- Now we have the breakdown
		INSERT INTO #tempOrderDetailBreakdown (OrderID, OrderDeatilID, SizeName, Qty)
		SELECT OrderID, OrderDeatilID, SizeName, Qty  
		FROM #tempOrderDeatilsWithQuantity 
		UNION ALL
		SELECT OrderID, OrderDeatilID, SizeName, Qty 
		FROM OrderDetailItemsCTE 
		ORDER BY OrderID, OrderDeatilID, SizeName, Qty

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
		SELECT		@ShipmentDetailId,
					od.ID,
					od.ID,
					NULL,
					ot.[Name],
					dis.[Name],
					c.[Name],
					o.[PurchaseOrderNo],
					vl.[NamePrefix],
					o.[ID],
					p.[NickName],
					i.[Name],
					t.SizeName,
					odq.Qty,
					t.Qty,
					os.Name,
					0,
					CASE pti.ID WHEN NULL THEN '' ELSE 'http://gw.indiman.net/IndicoData/PatternTemplates/' + CAST(p.ID AS nvarchar(8)) + '/' + pti.[Filename] + pti.Extension END,
					CASE im.ID WHEN NULL THEN '' ELSE 'http://gw.indiman.net/IndicoData/VisualLayout/' + CAST(vl.ID AS nvarchar(8)) + '/' + im.[Filename] + im.Extension END,
					p.[Number]
		FROM [Indico].[dbo].[OrderDetail] od
			INNER JOIN #tempOrderDetailBreakdown t
				ON t.OrderDeatilID = od.ID
			INNER JOIN [Indico].[dbo].[OrderDetailQty] odq
				ON od.[ID] = odq.[OrderDetail]	
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
			INNER JOIN [Indico].[dbo].[Item] i
				ON p.[SubItem] = i.ID
			INNER JOIN [Indico].[dbo].[Client] c
				ON o.Client = c.ID					
			LEFT OUTER JOIN [IndicoPacking].[dbo].[OrderDeatilItem] odi	
				ON t.OrderID = odi.IndicoOrderID
					AND t.OrderDeatilID = odi.IndicoOrderDeatilID
					AND t.SizeName = odi.SizeDesc
					AND t.Qty = odi.SizeSrno
			LEFT OUTER JOIN [Indico].[dbo].[PatternTemplateImage] pti
				ON p.ID = pti.Pattern
			LEFT OUTER JOIN [Indico].[dbo].[Image] im
				ON vl.ID = im.VisualLayout	
		WHERE odi.ID IS NULL AND (pti.ID IS NULL OR (pti.ID IS NOT NULL AND pti.IsHero = 1)) AND (im.ID IS NULL OR (im.ID IS NOT NULL AND im.IsHero = 1))

		-- Modify the OrderDetailItems


		-- Delete the OrderDetailsItems


		-- Delete the top record of shipment detail
		DELETE #tempShipmentDeatils
		FROM #tempShipmentDeatils
		WHERE ID = @ShipmentDetailId
				AND IndicoDistributorClientAddress = @IndicoDistributorClientAddress
	END

	IF OBJECT_ID('tempdb..#tempShipmentDeatils') IS NOT NULL 
	DROP Table #tempShipmentDeatils

	IF OBJECT_ID('tempdb..#tempOrderDetailBreakdown') IS NOT NULL 
		DROP Table #tempOrderDetailBreakdown

	IF OBJECT_ID('tempdb..#tempShipmentDeatils') IS NOT NULL 
	DROP Table #tempShipmentDeatils
END