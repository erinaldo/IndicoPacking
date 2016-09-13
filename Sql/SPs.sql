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

/****** Object:  StoredProcedure [dbo].[SPC_SynchroniseOrders]    Script Date: 22-Oct-15 4:34:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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

	IF OBJECT_ID('tempdb..#tempOrderDeatilsWithQuantity') IS NOT NULL 
		DROP Table #tempOrderDeatilsWithQuantity

	IF OBJECT_ID('tempdb..#tempOrderDetailBreakdown') IS NOT NULL 
		DROP Table #tempOrderDetailBreakdown

	IF OBJECT_ID('tempdb..#tempShipmentDeatils') IS NOT NULL 
		DROP Table #tempShipmentDeatils

	IF OBJECT_ID('tempdb..#tempDeletedOrderDetails') IS NOT NULL 
		DROP Table #tempDeletedOrderDetails

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
			dca.ID AS DistributorClientAddressId,
			dca.CompanyName,
			COALESCE(dp.Name, '') AS DestinationPort,
			COALESCE(sm.Name, '') AS ShipmentMode,
			COALESCE(pm.Name,'') AS PaymentMethod,
			od.ShipmentDate AS ETD,
			0 AS Qty,
			0 AS QuantityFilled,
			0 AS QuantityYetToBeFilled,
			'' AS InvoiceNo
	FROM [Indico].[dbo].[OrderDetail] od
		INNER JOIN [Indico].[dbo].[DistributorClientAddress] dca
			ON od.[DespatchTo] = dca.[ID]
		LEFT OUTER JOIN [Indico].[dbo].[DestinationPort] dp
			ON dca.[Port] = dp.[ID]
		LEFT OUTER JOIN [Indico].[dbo].[ShipmentMode] sm
			ON od.[ShipmentMode] = sm.[ID]	
		LEFT OUTER JOIN [Indico].[dbo].[PaymentMethod] pm
			ON od.[PaymentMethod] = pm.[ID]		
		LEFT OUTER JOIN [IndicoPacking].[dbo].[ShipmentDetail] sd
			ON dca.ID = sd.IndicoDistributorClientAddress
				AND sd.Shipment = @ShipmentId			
	WHERE (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, @WeekEndDate)) AND CAST(@WeekEndDate as DATE)) AND sd.ID IS NULL
	GROUP BY dca.ID,
			 dca.CompanyName,
			 dp.Name,
			 sm.Name,
			 pm.Name,
			 od.ShipmentDate

	-- ShipmentDetail records
	SELECT	sd.ID,
			sd.IndicoDistributorClientAddress,
			sd.Port,
			sd.ShipmentMode,
			sd.PriceTerm,
			sd.ETD INTO #tempShipmentDeatils
	FROM [IndicoPacking].[dbo].[ShipmentDetail] sd
	WHERE sd.Shipment = @ShipmentId

	CREATE TABLE #tempOrderDetailBreakdown
	(
		OrderID int NOT NULL, 
		OrderDeatilID int NOT NULL, 
		SizeName nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL, 
		Total int NOT NULL,
		Qty int NOT NULL
	)

	ALTER TABLE #tempOrderDetailBreakdown ADD PRIMARY KEY NONCLUSTERED (OrderID, OrderDeatilID, SizeName, Total, Qty)

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
	ORDER BY OrderID, OrderDeatilID, SizeName--, Total, Qty
	OPTION (MAXRECURSION 0);

	DECLARE @ShipmentDetailId int
	DECLARE @IndicoDistributorClientAddress int
	DECLARE @Port nvarchar(255)
	DECLARE @ShipmentMode nvarchar(64)
	DECLARE @PriceTerm nvarchar(64)
	DECLARE @ETD datetime2(7)
	DECLARE @QtyInShipmentDetail int
	DECLARE @CurrentQty int
	DECLARE @DeletedFilledItemsCount int

	WHILE (EXISTS(SELECT TOP 1 ID FROM #tempShipmentDeatils))
	BEGIN
		-- Get the top 1 from shipment detail 
		SELECT TOP 1 @ShipmentDetailId = ID,
					@IndicoDistributorClientAddress = IndicoDistributorClientAddress,
					@Port = Port,
					@ShipmentMode = ShipmentMode,
					@PriceTerm = PriceTerm,
					@ETD = ETD
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
				   ,[Fabric] 
				   ,[Material]
				   ,[Gender]
				   ,[AgeGroup]
				   ,[SleeveShape]
				   ,[SleeveLength]
				   ,[ItemSubGroup]
				   ,[SizeDesc]
				   ,[SizeQty]
				   ,[SizeSrno]
				   ,[Status]
				   ,[PrintedCount]
				   ,[PatternImage]
				   ,[VLImage]
				   ,[PatternNumber]
				   ,[OtherCharges]
				   ,[Notes]
				   ,[PatternInvoiceNotes]
				   ,[ProductNotes]
				   ,[JKFOBCostSheetPrice]
				   ,[IndimanCIFCostSheetPrice]
				   ,[HSCode]
				   ,[ItemName]
				   ,[PurchaseOrderNo])	
		SELECT		@ShipmentDetailId AS ShipmentDetailId,
					o.ID AS OrderId,
					od.ID AS OrderDetailId,
					NULL AS ShipmentDetailCarton,
					ot.[Name] AS OrderType,
					dis.[Name] AS Distributor,
					c.[Name] AS Client,
					'PO-' + CAST(o.ID AS nvarchar(47)) AS PurchaseOrder,
					vl.[NamePrefix],
					o.[ID] AS PurchaseOrder,
					p.[Number] + ' - ' + p.[NickName] AS Pattern,
					fc.[Code] + ' - ' + fc.[NickName] AS Fabric,
					fc.[Filaments] AS Material,
					g.Name AS Gender,
					ag.Name AS AgeGroup,
					'' AS SleeveShape,
					'' AS SleeveLength,
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
					) AS VLImagePath,
					p.[Number],
					0.0 AS OtherCharges,
					'' AS Notes,
					'' AS PatternInvoiceNotes,
					'' AS ProductNotes,
					COALESCE(cs.QuotedFOBCost, 0.0) AS JKFOBCostSheetPrice,
					COALESCE(cs.QuotedCIF, 0.0) AS IndimanCIFCostSheetPrice,
					ISNULL(CAST((SELECT CASE
										WHEN (p.[SubItem] IS NULL)
											THEN  	('')
										ELSE (CAST((SELECT TOP 1 hsc.[Code] FROM [Indico].[dbo].[HSCode] hsc WHERE hsc.[ItemSubCategory] = p.[SubItem] AND hsc.[Gender] = p.[Gender]) AS nvarchar(64)))
								END) AS nvarchar (64)), '') AS HSCode,
					ISNULL(CAST((SELECT CASE
										WHEN (p.[SubItem] IS NULL)
											THEN  	('')
										ELSE (CAST((SELECT it.[Name] FROM [Indico].[dbo].[Item] it WHERE it.[ID] = i.[Parent]) AS nvarchar(64)))
								END) AS nvarchar (64)), '') AS ItemName,
					o.PurchaseOrderNo
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
			INNER JOIN [Indico].[dbo].[Pattern] p
				ON od.Pattern = p.ID
			INNER JOIN [Indico].[dbo].[FabricCode] fc
				ON od.FabricCode = fc.ID
			LEFT OUTER JOIN [Indico].[dbo].[Gender] g
				ON p.Gender = g.ID
			LEFT OUTER JOIN [Indico].[dbo].[AgeGroup] ag
				ON p.AgeGroup = ag.ID
			LEFT OUTER JOIN [Indico].[dbo].[Item] i
				ON p.[SubItem] = i.ID
			INNER JOIN [Indico].[dbo].[Client] c
				ON o.Client = c.ID
			LEFT OUTER JOIN [Indico].[dbo].[ShipmentMode] sm
				ON sm.[ID] = od.ShipmentMode
			LEFT OUTER JOIN [Indico].[dbo].[PaymentMethod] pm
				ON pm.[ID] = od.PaymentMethod	
			LEFT OUTER JOIN [Indico].[dbo].[CostSheet] cs	
				ON p.ID = cs.Pattern
					AND fc.ID = cs.Fabric
			/*LEFT OUTER JOIN [Indico].[dbo].[Item] it
				ON it.Parent = i.ID	*/						
		WHERE odi.ID IS NULL AND od.DespatchTo = @IndicoDistributorClientAddress AND sm.Name = @ShipmentMode AND pm.Name = @PriceTerm AND od.[ShipmentDate] = @ETD
		ORDER BY o.ID, odi.IndicoOrderDetailID, t.SizeName, t.Total, t.Qty

		-- Modify the OrderDetailItems

		
		-- Delete the OrderDetailsItems
		SELECT odi.* INTO #tempDeletedOrderDetails	
		FROM [IndicoPacking].[dbo].[OrderDeatilItem] odi	
			LEFT OUTER JOIN #tempOrderDetailBreakdown t
				ON t.OrderID = odi.IndicoOrderID
					AND t.OrderDeatilID = odi.IndicoOrderDetailID
					AND t.SizeName = odi.SizeDesc
					AND t.Qty = odi.SizeSrno
			INNER JOIN	[Indico].[dbo].[OrderDetail] od
				ON t.OrderDeatilID = od.ID
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
			INNER JOIN [Indico].[dbo].[Pattern] p
				ON od.Pattern = p.ID
			INNER JOIN [Indico].[dbo].[FabricCode] fc
				ON od.FabricCode = fc.ID
			LEFT OUTER JOIN [Indico].[dbo].[Gender] g
				ON p.Gender = g.ID
			LEFT OUTER JOIN [Indico].[dbo].[AgeGroup] ag
				ON p.AgeGroup = ag.ID
			LEFT OUTER JOIN [Indico].[dbo].[Item] i
				ON p.[SubItem] = i.ID
			INNER JOIN [Indico].[dbo].[Client] c
				ON o.Client = c.ID
			LEFT OUTER JOIN [Indico].[dbo].[ShipmentMode] sm
				ON sm.[ID] = od.ShipmentMode
			LEFT OUTER JOIN [Indico].[dbo].[PaymentMethod] pm
				ON pm.[ID] = od.PaymentMethod								
		WHERE t.OrderDeatilID IS NULL AND od.DespatchTo = @IndicoDistributorClientAddress AND sm.Name = @ShipmentMode AND ((@PriceTerm IS NULL AND pm.Name IS NULL) OR pm.Name = @PriceTerm) AND od.[ShipmentDate] = @ETD

		-- Get the deleted filled count
		SELECT @DeletedFilledItemsCount = COUNT(*)
		FROM #tempDeletedOrderDetails t
		WHERE t.IsPolybagScanned = 1

		-- Now delete the deleted ones from order deatil item table
		DELETE [IndicoPacking].[dbo].[OrderDeatilItem] 
		FROM [IndicoPacking].[dbo].[OrderDeatilItem] odi
			JOIN #tempDeletedOrderDetails t
				ON odi.ID = t.ID

		-- Update the quantity
		-- Get the Qty in ShipmentDeatil
		SELECT @QtyInShipmentDetail = Qty
		FROM [IndicoPacking].[dbo].[ShipmentDetail]
		WHERE ID = @ShipmentDetailId

		SELECT @CurrentQty = COUNT(*) 
		FROM [IndicoPacking].[dbo].[ShipmentDetail] sd
			JOIN [IndicoPacking].[dbo].[OrderDeatilItem] odi
				ON sd.ID = odi.ShipmentDeatil
		WHERE sd.ID = @ShipmentDetailId
				AND sd.IndicoDistributorClientAddress = @IndicoDistributorClientAddress
				AND sd.Port = @Port
				AND sd.ShipmentMode = @ShipmentMode
				AND sd.PriceTerm = @PriceTerm
				AND sd.ETD = @ETD
	
		IF (@QtyInShipmentDetail < @CurrentQty)
		BEGIN
			UPDATE [IndicoPacking].[dbo].[ShipmentDetail]
				SET QuantityYetToBeFilled = QuantityYetToBeFilled + (@CurrentQty - @QtyInShipmentDetail),
					Qty = @CurrentQty
			WHERE ID = @ShipmentDetailId
		END
		ELSE IF (@QtyInShipmentDetail > @CurrentQty)
		BEGIN
			-- Here bit complcated as Need to find out which ones are deleted. If deleted ones already filled then need to remove them
			-- Update the counts
			UPDATE [IndicoPacking].[dbo].[ShipmentDetail]
				SET QuantityYetToBeFilled = QuantityYetToBeFilled - @DeletedFilledItemsCount,
					QuantityFilled = QuantityFilled - @DeletedFilledItemsCount,
					Qty = @CurrentQty
			WHERE ID = @ShipmentDetailId
		END

		-- Delete the top record of shipment detail
		DELETE #tempShipmentDeatils
		FROM #tempShipmentDeatils
		WHERE ID = @ShipmentDetailId
				AND IndicoDistributorClientAddress = @IndicoDistributorClientAddress
				AND Port = @Port
				AND ShipmentMode = @ShipmentMode
				AND PriceTerm = @PriceTerm
				AND ETD = @ETD

		IF OBJECT_ID('tempdb..#tempDeletedOrderDetails') IS NOT NULL 
			DROP Table #tempDeletedOrderDetails
	END

	IF OBJECT_ID('tempdb..#tempOrderDeatilsWithQuantity') IS NOT NULL 
		DROP Table #tempOrderDeatilsWithQuantity

	IF OBJECT_ID('tempdb..#tempOrderDetailBreakdown') IS NOT NULL 
		DROP Table #tempOrderDetailBreakdown

	IF OBJECT_ID('tempdb..#tempShipmentDeatils') IS NOT NULL 
		DROP Table #tempShipmentDeatils
END

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPC_GetOrderDetailsQuatityCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SPC_GetOrderDetailsQuatityCount]
GO

/****** Object:  StoredProcedure [dbo].[SPC_GetOrderDetailsQuatityCount]    Script Date: 07-Oct-15 6:26:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	Siwanka De Silva
-- Create date: 31st August 2015
-- Description:	This SP will return the order details count for given week
-- =============================================
CREATE PROCEDURE [dbo].[SPC_GetOrderDetailsQuatityCount] 
	@WeekNo int,
	@WeekEndDate datetime2(7)
AS
BEGIN
	SELECT 	SUM(odq.Qty) AS Total
	FROM [Indico].[dbo].[OrderDetail] od
		INNER JOIN [Indico].[dbo].[OrderDetailQty] odq
			ON od.[ID] = odq.[OrderDetail]		
	WHERE (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, @WeekEndDate)) AND CAST(@WeekEndDate as DATE)) 

END

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**




