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