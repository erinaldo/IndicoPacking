Use [IndicoPacking]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[UserDetailsView]'))
DROP VIEW [dbo].[UserDetailsView]
GO

/****** Object:  View [dbo].[UserDetailsView]    Script Date: 7/10/2015 11:43:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[UserDetailsView]
AS

SELECT u.[ID] 
      ,u.[Username]
      ,u.[Name]
	  ,us.[Name] AS [Status]
	  ,r.[Name] AS [Role]
      ,u.[EmailAddress]    
      ,u.[MobileTelephoneNumber]
      ,u.[OfficeTelephoneNumber]
      ,u.[GenderMale]
      ,u.[CreatedDate]
	  ,u.[DateLastLogin]
  FROM [dbo].[User] u
	JOIN [dbo].[UserStatus] us
		ON u.[Status] = us.[ID]	
	JOIN [dbo].[Role] r
		ON u.[Role] = r.[ID]

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceDetailsView]'))
DROP VIEW [dbo].[InvoiceDetailsView]
GO

/****** Object:  View [dbo].[InvoiceDetailsView]    Script Date: 15-Oct-15 12:40:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[InvoiceDetailsView]
AS

SELECT i.[ID],   
	   i.IndimanInvoiceDate,
	   i.IndimanInvoiceNumber,
	   i.FactoryInvoiceDate,
	   i.FactoryInvoiceNumber,	
	   CAST (YEAR (s.WeekendDate) % 100 AS VARCHAR(4))+ '/' + CAST (s.WeekNo AS VARCHAR(2))AS [Week],
	   CAST (YEAR (s.WeekendDate) % 100 AS VARCHAR(4))+ '/' + CAST (MONTH (s.WeekendDate)AS VARCHAR(2))AS [Month],  	    
	   i.ShipmentDate,
	   dca.CompanyName,
	   p.Name AS PortName,
	   m.Name AS ShipmentModeName,
	   i.BillTo,
	   i.AWBNumber,
	   i.ModifiedDate,
	   i.LastModifiedBy,		   
	   st.Name AS StatusName	   
  FROM	[dbo].[Invoice] i
		JOIN [dbo].[DistributorClientAddress] dca
			ON i.ShipTo = dca.ID
        JOIN [dbo].[ShipmentMode] m
            ON i.ShipmentMode = m.ID
        JOIN [dbo].[InvoiceStatus] st
            ON i.Status = st.ID
        JOIN [dbo].[Port] p
            ON i.Port = p.ID
		JOIN [dbo].[ShipmentDetail] sd
			ON i.ShipmentDetail = sd.ID
		JOIN [dbo].[Shipment] s
			ON sd.Shipment = s.ID

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetShipmentKeysView]'))
DROP VIEW [dbo].[GetShipmentKeysView]
GO

/****** Object:  View [dbo].[GetShipmentKeysView]    Script Date: 16-Oct-15 6:31:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetShipmentKeysView]
AS

SELECT 	s.ID, 
		s.ShipTo, 
		s.Shipment, 
		s.ETD, 
		s.Port, 
		s.ShipmentMode, 
		COUNT(o.ID) AS AvailableQuantity
FROM [dbo].[ShipmentDetail] s
	JOIN [dbo].[OrderDeatilItem] o
		ON s.[ID] = o.[ShipmentDeatil]	
WHERE o.Invoice IS NULL
GROUP BY s.ID, 
		 s.ShipTo, 
		 s.Shipment, 
		 s.ETD, 
		 s.Port, 
		 s.ShipmentMode

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceOrderDetailItemsWithQuatityBreakdown]'))
DROP VIEW [dbo].[GetInvoiceOrderDetailItemsWithQuatityBreakdown]
GO

/****** Object:  View [dbo].[GetInvoiceOrderDetailItemsWithQuatityBreakdown]    Script Date: 19-Oct-15 12:59:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetInvoiceOrderDetailItemsWithQuatityBreakdown]
AS

SELECT	  o.PurchaseOrder,
		  o.ID,
		  o.IndicoOrderID, 
		  o.IndicoOrderDetailID, 
		  o.OrderNumber, 
		  o.OrderType, 
		  o.VisualLayout, 
		  o.Pattern, 
		  o.Fabric,
		  o.Gender,
		  o.AgeGroup,
		  o.SleeveShape,
		  o.SleeveLength,
		  o.SizeDesc, 
		  o.SizeQty, 
		  o.SizeSrno, 
		  o.Distributor, 
		  o.Client, 
		  COALESCE(o.FactoryPrice, 0.0) AS FactoryPrice,
		  COALESCE(o.IndimanPrice, 0.0) AS IndimanPrice,
		  o.ShipmentDeatil,
		  o.Invoice,	
		  o.OtherCharges,
		  o.JKFOBCostSheetPrice,
		  o.IndimanCIFCostSheetPrice,
		  o.Notes
FROM [dbo].[OrderDeatilItem] o

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForFactory]'))
DROP VIEW [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForFactory]
GO

/****** Object:  View [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForFactory]    Script Date: 19-Oct-15 12:59:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForFactory]
AS

SELECT	o.PurchaseOrder,
		o.IndicoOrderID, 
		o.IndicoOrderDetailID, 
		o.OrderType, 
		o.VisualLayout, 
		o.Pattern,  
		o.Fabric,
		o.Gender,
		o.AgeGroup,
		o.SleeveShape,
		o.SleeveLength,
		COUNT(o.SizeQty) AS Qty, 
		o.Distributor, 
		o.Client, 
		o.ShipmentDeatil,
		o.Invoice,
		COALESCE(o.FactoryPrice, 0.0) AS FactoryPrice,
		o.JKFOBCostSheetPrice,
		o.OtherCharges,
		o.Notes		
FROM [dbo].[OrderDeatilItem] o
GROUP BY 
		o.PurchaseOrder,
		o.IndicoOrderID, 
		o.IndicoOrderDetailID, 
		o.OrderType, 
		o.VisualLayout, 
		o.Pattern,  
		o.Fabric,
		o.Gender,
		o.AgeGroup,
		o.SleeveShape,
		o.SleeveLength,
		o.Distributor, 
		o.Client, 
		o.ShipmentDeatil,
		o.Invoice,
		o.FactoryPrice,
		o.JKFOBCostSheetPrice,
		o.OtherCharges,
		o.Notes

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman]'))
DROP VIEW [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman]
GO

/****** Object:  View [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman]    Script Date: 19-Oct-15 12:59:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman]
AS

SELECT	o.PurchaseOrder,
		o.IndicoOrderID, 
		o.IndicoOrderDetailID, 
		o.OrderType, 
		o.VisualLayout, 
		o.Pattern,  
		o.Fabric,
		o.Gender,
		o.AgeGroup,
		o.SleeveShape,
		o.SleeveLength,
		COUNT(o.SizeQty) AS Qty, 
		o.Distributor, 
		o.Client, 
		o.ShipmentDeatil,
		o.Invoice,
		COALESCE(o.FactoryPrice, 0.0) AS FactoryPrice,
		o.JKFOBCostSheetPrice,
		COALESCE(o.IndimanPrice, 0.0) AS IndimanPrice,
		o.IndimanCIFCostSheetPrice,
		o.OtherCharges,
		o.Notes		
FROM [dbo].[OrderDeatilItem] o
GROUP BY 
		o.PurchaseOrder,
		o.IndicoOrderID, 
		o.IndicoOrderDetailID, 
		o.OrderType, 
		o.VisualLayout, 
		o.Pattern,  
		o.Fabric,
		o.Gender,
		o.AgeGroup,
		o.SleeveShape,
		o.SleeveLength,
		o.Distributor, 
		o.Client, 
		o.ShipmentDeatil,
		o.Invoice,
		o.FactoryPrice,
		o.JKFOBCostSheetPrice,
		o.IndimanPrice,
		o.IndimanCIFCostSheetPrice,
		o.OtherCharges,
		o.Notes

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetWeeklyAddressDetails]'))
DROP VIEW [dbo].[GetWeeklyAddressDetails]
GO

/****** Object:  View [dbo].[GetWeeklyAddressDetails]    Script Date: 13-Nov-15 2:22:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetWeeklyAddressDetails]
AS

SELECT	i.ID AS Invoice,
		o.IndicoOrderDetailID AS OrderDetail, 
		o.OrderType, 
		o.VisualLayout, 
		o.Pattern,  
		o.Fabric,
		o.IndicoOrderID AS [Order],
		s.ETD AS ShipmentDate,
		COUNT(o.SizeSrno) AS Qty,
		o.PurchaseOrder,
		o.Distributor, 
		o.Client,
		o.[Status],
		o.SizeDesc,
		SUM(o.FactoryPrice) AS TotalPrice,
		s.ShipmentMode,
		s.ShipTo AS CompanyName,
		d.[Address],
		d.Suburb,
		d.[State],
		d.PostCode,		 
		c.ShortName AS Country,
		ISNULL(d.[ContactName],'') + ' ' + ISNULL(d.[ContactPhone],'') AS ContactDetails,
		o.HSCode,
		o.ItemSubGroup,
		o.Gender,
		o.ItemName
FROM [dbo].[OrderDeatilItem] o
	INNER JOIN [dbo].[ShipmentDetail] s
		ON o.ShipmentDeatil = s.ID
	INNER JOIN [dbo].[Invoice] i
		ON i.ShipmentDetail = s.ID
	INNER JOIN [dbo].[DistributorClientAddress] d
		ON i.ShipTo = d.ID
	INNER JOIN [dbo].[Country] c
		ON d.Country = c.ID
GROUP BY i.ID,
		o.IndicoOrderDetailID, 
		o.OrderType, 
		o.VisualLayout, 
		o.Pattern,  
		o.Fabric,
		o.IndicoOrderID,
		s.ETD,
		o.PurchaseOrder,
		o.Distributor, 
		o.Client,
		o.[Status],
		o.SizeDesc,
		o.FactoryPrice,
		s.ShipmentMode,
		s.ShipTo,
		d.[Address],
		d.Suburb,
		d.[State],
		d.PostCode,		 
		c.ShortName,
		ISNULL(d.[ContactName],'') + ' ' + ISNULL(d.[ContactPhone],''),
		o.HSCode,
		o.ItemSubGroup,
		o.Gender,
		o.ItemName


GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetWeeklyAddressDetailsByDistributor]'))
DROP VIEW [dbo].[GetWeeklyAddressDetailsByDistributor]
GO

/****** Object:  View [dbo].[GetWeeklyAddressDetailsByDistributor]    Script Date: 17-Nov-15 7:13:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[GetWeeklyAddressDetailsByDistributor]
AS

SELECT t.*,
		(	SELECT oi.SizeDesc + '/' + CAST(oi.SizeQty AS nvarchar(5)) + ', ' AS 'data()' 
				FROM [dbo].[OrderDeatilItem] oi
				WHERE  oi.IndicoOrderDetailID = t.IndicoOrderDetailID
				GROUP BY oi.SizeDesc, oi.SizeQty
				FOR XML PATH('')) AS SizeQuantities
FROM (
	SELECT	i.ID,
			o.IndicoOrderDetailID,
			o.Distributor,
			o.Client,
			(o.OrderType + ' ' + (ISNULL(o.PurchaseOrderNo, ''))) AS OrderType,
			COUNT(o.SizeSrno) AS Qty,
			o.Gender + ' ' + o.AgeGroup AS GenderAgeGroup,
			o.Material,
			o.FactoryPrice,
			o.OtherCharges,
			(o.FactoryPrice + o.OtherCharges) AS TotalPrice,
			(COUNT(o.SizeSrno) * (o.FactoryPrice + o.OtherCharges)) AS Amount,
			o.PurchaseOrder,
			o.PurchaseOrderNo,		
			o.Fabric,
			o.VisualLayout,
			o.ItemName
	FROM [dbo].[OrderDeatilItem] o
		INNER JOIN [dbo].[ShipmentDetail] s
			ON o.ShipmentDeatil = s.ID
		INNER JOIN [dbo].[Invoice] i
			ON i.ShipmentDetail = s.ID
	GROUP BY i.ID,
			o.IndicoOrderDetailID,
			o.Distributor,
			o.Client,
			(o.OrderType + ' ' + (ISNULL(o.PurchaseOrderNo, ''))),
			o.Gender + ' ' + o.AgeGroup,
			o.Material,
			o.FactoryPrice,
			o.OtherCharges,
			o.FactoryPrice + o.OtherCharges,
			o.PurchaseOrder,
			o.PurchaseOrderNo,
			o.Fabric,
			o.VisualLayout,
			o.ItemName ) t

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetWeeklyAddressDetailsByHSCode]'))
DROP VIEW [dbo].[GetWeeklyAddressDetailsByHSCode]
GO

/****** Object:  View [dbo].[GetWeeklyAddressDetailsByHSCode]    Script Date: 17-Nov-15 7:13:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[GetWeeklyAddressDetailsByHSCode]
AS

SELECT	i.ID,
		ISNULL(o.HSCode, '') AS HSCode,
		ISNULL(o.Material, '') AS Material,
		ISNULL(o.ItemName, '') AS ItemName,
		ISNULL(o.Gender, '') AS Gender,
		ISNULL(o.ItemSubGroup, '') AS ItemSubGroup,
		COUNT(o.SizeSrno) AS Qty,
		SUM(o.FactoryPrice) AS TotalPrice
FROM [dbo].[OrderDeatilItem] o
	INNER JOIN [dbo].[ShipmentDetail] s
		ON o.ShipmentDeatil = s.ID
	INNER JOIN [dbo].[Invoice] i
		ON i.ShipmentDetail = s.ID
GROUP BY i.ID,
		o.HSCode,
		o.Material,
		o.ItemName,
		o.Gender,
		o.ItemSubGroup


GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetWeeklyAddressDetailsByDistributorForIndiman]'))
DROP VIEW [dbo].[GetWeeklyAddressDetailsByDistributorForIndiman]
GO

/****** Object:  View [dbo].[GetWeeklyAddressDetailsByDistributorForIndiman]    Script Date: 19-Nov-15 5:46:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[GetWeeklyAddressDetailsByDistributorForIndiman]
AS

SELECT t.*,
		(	SELECT oi.SizeDesc + '/' + CAST(oi.SizeQty AS nvarchar(5)) + ', ' AS 'data()' 
				FROM [dbo].[OrderDeatilItem] oi
				WHERE  oi.IndicoOrderDetailID = t.IndicoOrderDetailID
				GROUP BY oi.SizeDesc, oi.SizeQty
				FOR XML PATH('')) AS SizeQuantities
FROM (
	SELECT	i.ID,
			o.IndicoOrderDetailID,
			o.Distributor,
			o.Client,
			o.OrderType,
			COUNT(o.SizeSrno) AS Qty,
			o.Gender + ' ' + o.AgeGroup AS GenderAgeGroup,
			o.Material,
			o.IndimanPrice,
			o.OtherCharges,
			(o.IndimanPrice + o.OtherCharges) AS TotalPrice,
			(COUNT(o.SizeSrno) * (o.IndimanPrice + o.OtherCharges)) AS Amount,
			o.PurchaseOrder,		
			o.Fabric,						    
			o.Pattern,
		    LEFT(o.Pattern, CHARINDEX('-', o.Pattern) - 1) AS PatternNo,
			REPLACE(SUBSTRING(o.Pattern, CHARINDEX('-', o.Pattern), LEN(o.Pattern)), '-', '') AS NickName,
			REPLACE(SUBSTRING(o.Fabric, CHARINDEX('-', o.Fabric), LEN(o.Fabric)), '-', '') AS FabricName,
			o.VisualLayout,
			o.ItemName,
			o.Notes
	FROM [dbo].[OrderDeatilItem] o
		INNER JOIN [dbo].[ShipmentDetail] s
			ON o.ShipmentDeatil = s.ID
		INNER JOIN [dbo].[Invoice] i
			ON i.ShipmentDetail = s.ID
	GROUP BY i.ID,
			o.IndicoOrderDetailID,
			o.Distributor,
			o.Client,
			o.OrderType,
			o.Gender + ' ' + o.AgeGroup,
			o.Material,
			o.IndimanPrice,
			o.OtherCharges,
			o.IndimanPrice + o.OtherCharges,
			o.PurchaseOrder,
			o.Fabric,
			o.Pattern,
			o.VisualLayout,
			o.ItemName,
			o.Notes ) t

GO
--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderDetailsView]'))
DROP VIEW [dbo].[InvoiceHeaderDetailsView]
GO


/****** Object:  View [dbo].[[InvoiceHeaderDetailsView]]    Script Date: 26-Nov-15 4:22:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[InvoiceHeaderDetailsView]
AS

SELECT i.[ID],   
	   CAST (i.IndimanInvoiceDate AS DATE) AS [IndimanInvoiceDate],
	   i.IndimanInvoiceNumber,
	   CAST (i.FactoryInvoiceDate AS DATE) AS [FactoryInvoiceDate],
	   i.FactoryInvoiceNumber,	
	   CAST (YEAR (s.WeekendDate) % 100 AS VARCHAR(4))+ '/' + CAST (s.WeekNo AS VARCHAR(2))AS [Week],
	   CAST (YEAR (s.WeekendDate) % 100 AS VARCHAR(4))+ '/' + CAST (MONTH (s.WeekendDate)AS VARCHAR(2))AS [Month],  	    
	   CAST(i.ShipmentDate AS DATE) AS [ShipmentDate],
	   dca.CompanyName,
	   p.Name AS PortName,
	   m.Name AS ShipmentModeName,
	   i.BillTo,
	   i.AWBNumber,		   
	   st.Name AS StatusName,
	   dca.Address + ' ' + dca.State AS CompanyAddress,
	   dca.PostCode + ' ' + c.ShortName	AS CompanyPostalCode,
	   dca.ContactName + ' ' + dca.ContactPhone AS CompanyContact,
	   bdca.CompanyName AS BillToCompanyName,
	   bdca.Address AS BillToAddress,
	   bdca.Suburb +' '+ bdca.State + ' ' + bdca.PostCode AS BillToCompanyState,
	   bc.ShortName AS BillToCountry
  FROM	[dbo].[Invoice] i
		JOIN [dbo].[DistributorClientAddress] dca
			ON i.ShipTo = dca.ID
        JOIN [dbo].[ShipmentMode] m
            ON i.ShipmentMode = m.ID
        JOIN [dbo].[InvoiceStatus] st
            ON i.Status = st.ID
        JOIN [dbo].[Port] p
            ON i.Port = p.ID
		JOIN [dbo].[ShipmentDetail] sd
			ON i.ShipmentDetail = sd.ID
		JOIN [dbo].[Shipment] s
			ON sd.Shipment = s.ID
		JOIN [dbo].[Country] c
			ON dca.Country = c.ID
		JOIN [dbo].[DistributorClientAddress] bdca
			ON i.BillTo = bdca.ID
		JOIN [dbo].[Country] bc
			ON bdca.Country = bc.ID
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetCartonLabelInfo]'))
DROP VIEW [dbo].[GetCartonLabelInfo]
GO


/****** Object:  View [dbo].[GetCartonLabelInfo]    Script Date: 26-Nov-15 4:22:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetCartonLabelInfo]
AS

SELECT	t.ID,
		t.PurchaseOrder,
		t.VisualLayout,
		(	SELECT oi.SizeDesc + '/' + CAST( COUNT(oi.SizeQty) AS nvarchar(5)) + ',' AS 'data()' 
				FROM [dbo].[OrderDeatilItem] oi
				WHERE  oi.ShipmentDetailCarton = t.ID AND oi.IndicoOrderDetailID = t.IndicoOrderDetailID
				GROUP BY oi.SizeDesc, oi.SizeQty
				FOR XML PATH('')) AS SizeQuantities,
		(	SELECT COUNT(oi.SizeQty)
		FROM [dbo].[OrderDeatilItem] oi
		WHERE  oi.ShipmentDetailCarton = t.ID AND oi.IndicoOrderDetailID = t.IndicoOrderDetailID
		) AS Total,
		t.Client,
		t.Distributor,
		t.FactoryInvoiceNumber
FROM (
	SELECT	sdc.ID,
			odi.IndicoOrderDetailID,
			odi.PurchaseOrder, 
			odi.VisualLayout, 
			odi.Client, 
			odi.Distributor,
			i.FactoryInvoiceNumber
	FROM ShipmentDetailCarton sdc
		JOIN OrderDeatilItem odi
			ON sdc.ID = odi.ShipmentDetailCarton
		LEFT OUTER JOIN Invoice i
			ON odi.Invoice = i.ID
	GROUP BY sdc.ID, odi.IndicoOrderDetailID, odi.PurchaseOrder, odi.VisualLayout, odi.Client, odi.Distributor, i.FactoryInvoiceNumber ) t

GO