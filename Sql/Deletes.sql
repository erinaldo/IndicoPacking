/****** Script for SelectTopNRows command from SSMS  ******/


  UPDATE [IndicoPacking].[dbo].[OrderDeatilItem]
  SET ShipmentDetailCarton = null
  GO
  delete [IndicoPacking].[dbo].[ShipmentDetailCarton]
  delete [IndicoPacking].[dbo].[OrderDeatilItem]
  delete [IndicoPacking].[dbo].[ShipmentDetail]
  delete [IndicoPacking].[dbo].[Shipment]
  GO

  UPDATE [IndicoPacking].[dbo].[OrderDeatilItem]
  SET PrintedCount = 0
  GO
