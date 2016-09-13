USE [IndicoPacking]
GO

/****** Object:  StoredProcedure [dbo].[SPC_SynchroniseOrders]    Script Date: 19/10/2015 11:18:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		
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

	IF(NOT EXISTS(	SELECT ID
					FROM [IndicoPacking].[dbo].[Shipment] s
					WHERE s.WeekNo = @WeekNo AND s.WeekendDate = @WeekEndDate))
	BEGIN
		INSERT INTO [dbo].[Shipment]
           ([WeekNo]
           ,[WeekendDate]
           ,[IndicoWeeklyProductionCapacityID])
		VALUES
           (@WeekNo
           ,@WeekEndDate
           , (SELECT TOP 1 w.[ID] FROM [Indico].[dbo].[WeeklyProductionCapacity] w
			WHERE w.WeekNo = @WeekNo AND w.WeekendDate = @WeekEndDate))
	END

	-- Get the shipment Id	
	DECLARE @ShipmentId int

	SELECT @ShipmentId = ID
	FROM [IndicoPacking].[dbo].[Shipment] s
	WHERE s.WeekNo = @WeekNo AND s.WeekendDate = @WeekEndDate 

	-- Shipment Deatil
	DECLARE @AddressID int
	DECLARE @AddressCompanyName nvarchar(255) 
	DECLARE @Addressport nvarchar(255)
	DECLARE @AddressShipmentMode nvarchar(64)
	DECLARE @PaymentMethod nvarchar(64)

	DECLARE ShipmentDetailRecord CURSOR FAST_FORWARD
	FOR SELECT
			dca.ID,
			dca.CompanyName,
			dp.Name,
			sm.Name,
			pm.Name
	FROM [Indico].[dbo].[OrderDetail] od
		--INNER JOIN [Indico].[dbo].[Order] o
		--	ON od.[Order] = o.ID
		--INNER JOIN [Indico].[dbo].[OrderStatus] os
		--	ON o.[Status] = os.ID
		--JOIN [Indico].[dbo].[ShipmentMode] shm
		--	ON o.[ShipmentMode] = shm.[ID] 	
		JOIN [Indico].[dbo].[DistributorClientAddress] dca
			ON od.[DespatchTo] = dca.[ID]
		JOIN [Indico].[dbo].[DestinationPort] dp
			ON dca.[Port] = dp.[ID]
		JOIN [Indico].[dbo].[ShipmentMode] sm
			ON od.[ShipmentMode] = sm.[ID]	
		JOIN [Indico].[dbo].[PaymentMethod] pm
			ON od.[PaymentMethod] = pm.[ID]				
	WHERE (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, @WeekEndDate)) AND CAST(@WeekEndDate as DATE))--(od.[SheduledDate] BETWEEN CAST(DATEADD(WK, DATEDIFF(WK, 0, @WeekEndDate), 0) as DATE) AND DATEADD(DAY, 2, CONVERT (DATE, @WeekEndDate)))
	GROUP BY dca.ID,
			dca.CompanyName,
			dp.Name,
			sm.Name,
			pm.Name
				
	OPEN ShipmentDetailRecord

	FETCH FROM ShipmentDetailRecord INTO @AddressID, @AddressCompanyName, @Addressport, @AddressShipmentMode, @PaymentMethod
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		IF (NOT EXISTS(	SELECT ID FROM [IndicoPacking].[dbo].[ShipmentDetail] 
					WHERE Shipment = @ShipmentId 
						AND IndicoDistributorClientAddress = @AddressID 
						AND ShipTo = @AddressCompanyName 
						AND Port = @Addressport 
						AND ShipmentMode = @AddressShipmentMode 
						AND PriceTerm = @PaymentMethod))
		BEGIN
			
			-- Process the ShipmentDeatil table
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
			SELECT @ShipmentId,
					@AddressID,
					@AddressCompanyName,
					@Addressport,
					@AddressShipmentMode,
					@PaymentMethod,
					@WeekEndDate,
					(SELECT SUM(odq.Qty) 
						FROM [Indico].[dbo].[OrderDetailQty] odq
							JOIN [Indico].[dbo].[OrderDetail] od
								ON odq.OrderDetail = od.ID
							JOIN [Indico].[dbo].[DistributorClientAddress] dca
								ON od.DespatchTo = dca.ID
							JOIN [Indico].[dbo].[DestinationPort] dp
								ON dca.[Port] = dp.[ID]
							JOIN [Indico].[dbo].[ShipmentMode] sm
								ON od.[ShipmentMode] = sm.[ID]	
							JOIN [Indico].[dbo].[PaymentMethod] pm
								ON od.[PaymentMethod] = pm.[ID]
						WHERE dca.ID = @AddressID AND dca.CompanyName = @AddressCompanyName AND dp.Name = @Addressport AND sm.Name = @AddressShipmentMode AND pm.Name = @PaymentMethod AND odq.Qty != 0
							AND (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, @WeekEndDate)) AND CAST(@WeekEndDate as DATE))),--(od.[SheduledDate] BETWEEN CAST(DATEADD(WK, DATEDIFF(WK, 0, @WeekEndDate), 0) as DATE) AND DATEADD(DAY, 2, CONVERT (DATE, @WeekEndDate)))),
					0,
					0,					
					''
		END
		ELSE
		BEGIN 
			-- Now update the Quantity
			UPDATE [IndicoPacking].[dbo].[ShipmentDetail]
				SET [Qty] = (SELECT SUM(odq.Qty) 
							FROM [Indico].[dbo].[OrderDetailQty] odq
								JOIN [Indico].[dbo].[OrderDetail] od
									ON odq.OrderDetail = od.ID
								JOIN [Indico].[dbo].[DistributorClientAddress] dca
									ON od.DespatchTo = dca.ID
								JOIN [Indico].[dbo].[DestinationPort] dp
									ON dca.[Port] = dp.[ID]
								JOIN [Indico].[dbo].[ShipmentMode] sm
									ON od.[ShipmentMode] = sm.[ID]	
								JOIN [Indico].[dbo].[PaymentMethod] pm
									ON od.[PaymentMethod] = pm.[ID]
							WHERE dca.ID = @AddressID AND dca.CompanyName = @AddressCompanyName AND dp.Name = @Addressport AND sm.Name = @AddressShipmentMode AND pm.Name = @PaymentMethod AND odq.Qty != 0
								AND (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, @WeekEndDate)) AND CAST(@WeekEndDate as DATE)))--(od.[SheduledDate] BETWEEN CAST(DATEADD(WK, DATEDIFF(WK, 0, @WeekEndDate), 0) as DATE) AND DATEADD(DAY, 2, CONVERT (DATE, @WeekEndDate))))
			FROM [IndicoPacking].[dbo].[ShipmentDetail] 
			WHERE Shipment = @ShipmentId 
				AND IndicoDistributorClientAddress = @AddressID 
				AND ShipTo = @AddressCompanyName 
				AND Port = @Addressport 
				AND ShipmentMode = @AddressShipmentMode 
				AND PriceTerm = @PaymentMethod
		END
		
		FETCH FROM ShipmentDetailRecord INTO @AddressID, @AddressCompanyName, @Addressport, @AddressShipmentMode, @PaymentMethod
	END

	CLOSE ShipmentDetailRecord
	DEALLOCATE ShipmentDetailRecord

	-- Process the OrderDetailItem table
	DECLARE @ShipmentDetail int
	DECLARE @IndicoOrderID int
	DECLARE @IndicoOrderDetailID int
	DECLARE @OrderQuantity int
	DECLARE @SizeName nvarchar(255)
	DECLARE @Count int

	/*DECLARE @tOrderDetailRecord TABLE
	(
		ShipmentDetail int,
		IndicoOrderID int,
		IndicoOrderDetailID int,
		SizeName nvarchar(255),
		OrderQuantity int,
		UNIQUE CLUSTERED (ShipmentDetail, IndicoOrderID, IndicoOrderDetailID, SizeName, OrderQuantity)
	)

	INSERT INTO @tOrderDetailRecord
	SELECT  sd.[ID],
			o.ID AS OrderID,
			od.ID AS OrderDeatilID,
			s.SizeName,
			odq.Qty
	FROM  [IndicoPacking].[dbo].[Shipment] sh
		INNER JOIN [IndicoPacking].[dbo].[ShipmentDetail] sd
			ON sh.[ID] = sd.[Shipment]
		INNER JOIN [Indico].[dbo].[DistributorClientAddress] dca
			ON dca.[ID] = sd.[IndicoDistributorClientAddress]
		INNER JOIN [Indico].[dbo].[OrderDetail] od
			ON dca.[ID] = od.[DespatchTo]
		INNER JOIN [Indico].[dbo].[OrderDetailQty] odq
			ON od.[ID] = odq.[OrderDetail]		
		INNER JOIN [Indico].[dbo].[Order] o
			ON od.[Order] = o.ID
		INNER JOIN [Indico].[dbo].[Size] s
			ON odq.[Size] = s.[ID]
	WHERE (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, @WeekEndDate)) AND CAST(@WeekEndDate as DATE)) AND sh.ID = @ShipmentId and odq.Qty != 0--sh.WeekNo = @WeekNo AND sh.WeekendDate = @WeekEndDate --(od.[SheduledDate] BETWEEN CAST(DATEADD(WK, DATEDIFF(WK, 0, @WeekEndDate), 0) as DATE) AND DATEADD(DAY, 2, CONVERT (DATE, @WeekEndDate)))
	ORDER BY od.ID */

		FROM [Indico].[dbo].[OrderDetail] od
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
					LEFT OUTER JOIN [Indico].[dbo].[PatternTemplateImage] pti
						ON p.ID = pti.Pattern
					LEFT OUTER JOIN [Indico].[dbo].[Image] im
						ON vl.ID = im.VisualLayout		
				WHERE od.ID = @IndicoOrderDetailID AND (pti.ID IS NULL OR (pti.ID IS NOT NULL AND pti.IsHero = 1)) AND (im.ID IS NULL OR (im.ID IS NOT NULL AND im.IsHero = 1))


	;WITH OrderDetailItemsCTE (Supplier, Qty) AS (
		SELECT Supplier, Quantity - 1
		FROM @d
		WHERE Quantity > 1

		UNION ALL

		SELECT S.Supplier, I.Qty - 1
		FROM @d S
			INNER JOIN OrderDetailItemsCTE I ON I.Supplier = S.Supplier
		WHERE I.Qty > 1
	)

	SELECT Supplier, Quantity FROM @d 
	UNION all
	SELECT * FROM OrderDetailItemsCTE order by Supplier, Quantity

	
	/*WHILE (EXISTS(SELECT TOP 1 ShipmentDetail FROM @tOrderDetailRecord))
	BEGIN
		SELECT TOP 1 
			@ShipmentDetail = ShipmentDetail, 
			@IndicoOrderID = IndicoOrderID,  
			@IndicoOrderDetailID = IndicoOrderDetailID,
			@OrderQuantity = OrderQuantity,
			@SizeName = SizeName
		FROM @tOrderDetailRecord

		SET @Count = 0
		WHILE (@Count < @OrderQuantity)
		BEGIN
			IF (NOT EXISTS(	SELECT ID 
							FROM [IndicoPacking].[dbo].[OrderDeatilItem] 
							WHERE ShipmentDeatil = @ShipmentDetail 
								AND IndicoOrderID = @IndicoOrderID 
								AND IndicoOrderDetailID = @IndicoOrderDetailID
								AND SizeDesc = @SizeName 
								AND SizeSrno = @Count + 1))
			BEGIN
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
				   --,[Barcode]
				   --,[CartonNo]
				   --,[ScanCartonNo]
				   ,[PrintedCount]
				   ,[PatternImage]
				   ,[VLImage]
				   ,[PatternNumber])
				SELECT @ShipmentDetail,
					@IndicoOrderID,
					@IndicoOrderDetailID,
					NULL,
					ot.[Name],
					dis.[Name],
					c.[Name],
					o.[PurchaseOrderNo],
					vl.[NamePrefix],
					o.[ID],
					p.[NickName],
					i.[Name],
					@SizeName,
					@OrderQuantity,
					@Count + 1,
					os.Name,
					--'',
					--0,
					--0,
					0,
					CASE pti.ID WHEN NULL THEN '' ELSE 'http://gw.indiman.net/IndicoData/PatternTemplates/' + CAST(p.ID AS nvarchar(8)) + '/' + pti.[Filename] + pti.Extension END,
					CASE im.ID WHEN NULL THEN '' ELSE 'http://gw.indiman.net/IndicoData/VisualLayout/' + CAST(vl.ID AS nvarchar(8)) + '/' + im.[Filename] + im.Extension END,
					p.[Number]
				FROM [Indico].[dbo].[OrderDetail] od
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
					LEFT OUTER JOIN [Indico].[dbo].[PatternTemplateImage] pti
						ON p.ID = pti.Pattern
					LEFT OUTER JOIN [Indico].[dbo].[Image] im
						ON vl.ID = im.VisualLayout		
				WHERE od.ID = @IndicoOrderDetailID AND (pti.ID IS NULL OR (pti.ID IS NOT NULL AND pti.IsHero = 1)) AND (im.ID IS NULL OR (im.ID IS NOT NULL AND im.IsHero = 1))

				SET @Count = @Count + 1
			END
			ELSE
			BEGIN
				SET @Count = @Count + 1
			END
		END

		-- Delete the top record from table variable
		DELETE @tOrderDetailRecord
		FROM @tOrderDetailRecord
		WHERE ShipmentDetail = @ShipmentDetail AND 
			IndicoOrderID  = @IndicoOrderID AND
			IndicoOrderDetailID = @IndicoOrderDetailID AND
			SizeName = @SizeName
	END*/

	SELECT 1
END


GO


