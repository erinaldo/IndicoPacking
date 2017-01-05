USE IndicoPacking
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman]'))
	DROP VIEW [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman]
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
		ISNULL(o.JKFOBCostSheetPrice,0) AS JKFOBCostSheetPrice,
		COALESCE(o.IndimanPrice, 0.0) AS IndimanPrice,
		ISNULL(o.IndimanCIFCostSheetPrice, 0) IndimanCIFCostSheetPrice,
		o.OtherCharges,
		o.Notes,
		ISNULL(o.ProductNotes,'') AS ProductNotes,
		o.PatternInvoiceNotes AS PatternNotes
FROM [dbo].[OrderDeatilItem] o
	INNER JOIN [dbo].[ShipmentDetailCarton] sdc
        ON o.ShipmentDetailCarton = sdc.ID
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
		o.Notes,
		o.ProductNotes,
		o.PatternInvoiceNotes
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--

ALTER TABLE [dbo].[Invoice]
ADD CourierCharges int
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceHeaderDetailsView]'))
	DROP VIEW [dbo].[InvoiceHeaderDetailsView]
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
	   dca.[Address] + ' ' + dca.[Suburb] + ' ' + dca.[State] + ' ' + dca.PostCode + ' '  + c.ShortName  AS  CompanyAddress,
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

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceDetailsView]'))
	DROP VIEW  [dbo].[InvoiceDetailsView]
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
	   st.Name AS StatusName,
	   COUNT(odi.ID) AS Qty,
	   SUM((odi.OtherCharges + odi.IndimanPrice)) AS TotalAmount,
	   ISNULL(i.CourierCharges, 0) AS CourierCharges
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
		INNER JOIN [dbo].[OrderDeatilItem] odi
			ON odi.Invoice = i.ID
	GROUP BY i.[ID],   
		   i.IndimanInvoiceDate,
		   i.IndimanInvoiceNumber,
		   i.FactoryInvoiceDate,
		   i.FactoryInvoiceNumber,	
		   s.WeekendDate,
		   s.WeekNo,
		   i.ShipmentDate,
		   dca.CompanyName,
		   p.Name,
		   m.Name,
		   i.BillTo,
		   i.AWBNumber,
		   i.ModifiedDate,
		   i.LastModifiedBy,		   
		   st.Name,
		   i.CourierCharges
GO


--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--


  ALTER TABLE [dbo].OrderDetailsFromIndico 
	ALTER COLUMN JobName nvarchar(255) null

 ALTER TABLE [dbo].OrderDetailsFromIndico
	ALTER COLUMN PatternInvoiceNotes nvarchar(255) null

 ALTER TABLE [dbo].OrderDetailsFromIndico 
	ALTER COLUMN ProductNotes nvarchar(255) null

 ALTER TABLE [dbo].OrderDetailsFromIndico 
	ALTER COLUMN Notes nvarchar(255) null

GO