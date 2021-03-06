USE [IndicoPacking]
GO
/****** Object:  StoredProcedure [dbo].[SPC_SynchroniseOrders]    Script Date: 12/10/2015 7:54:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPC_SynchroniseOrders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SPC_SynchroniseOrders]
GO
/****** Object:  StoredProcedure [dbo].[SPC_GetOrderDetailsQuatityCount]    Script Date: 12/10/2015 7:54:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPC_GetOrderDetailsQuatityCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SPC_GetOrderDetailsQuatityCount]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_UserStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_UserStatus]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Role]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Role]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ShipmentDetailCarton_ShipmentDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[ShipmentDetailCarton]'))
ALTER TABLE [dbo].[ShipmentDetailCarton] DROP CONSTRAINT [FK_ShipmentDetailCarton_ShipmentDetail]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ShipmentDetailCarton_Carton]') AND parent_object_id = OBJECT_ID(N'[dbo].[ShipmentDetailCarton]'))
ALTER TABLE [dbo].[ShipmentDetailCarton] DROP CONSTRAINT [FK_ShipmentDetailCarton_Carton]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ShipmentDetail_Shipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[ShipmentDetail]'))
ALTER TABLE [dbo].[ShipmentDetail] DROP CONSTRAINT [FK_ShipmentDetail_Shipment]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDeatilItem_ShipmentDetailCarton]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDeatilItem]'))
ALTER TABLE [dbo].[OrderDeatilItem] DROP CONSTRAINT [FK_OrderDeatilItem_ShipmentDetailCarton]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDeatilItem_ShipmentDeatil]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDeatilItem]'))
ALTER TABLE [dbo].[OrderDeatilItem] DROP CONSTRAINT [FK_OrderDeatilItem_ShipmentDeatil]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OrderDeatilItem_Invoice]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderDeatilItem]'))
ALTER TABLE [dbo].[OrderDeatilItem] DROP CONSTRAINT [FK_OrderDeatilItem_Invoice]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_Status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_ShipTo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_ShipTo]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_ShipmentMode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_ShipmentMode]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_ShipmentDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_ShipmentDetail]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_Port]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_Port]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_LastModifiedBy]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_LastModifiedBy]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_BillTo]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_BillTo]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_Bank]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] DROP CONSTRAINT [FK_Invoice_Bank]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DistributorAddressr_Port]') AND parent_object_id = OBJECT_ID(N'[dbo].[DistributorClientAddress]'))
ALTER TABLE [dbo].[DistributorClientAddress] DROP CONSTRAINT [FK_DistributorAddressr_Port]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DistributorAddressr_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[DistributorClientAddress]'))
ALTER TABLE [dbo].[DistributorClientAddress] DROP CONSTRAINT [FK_DistributorAddressr_Country]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bank_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bank]'))
ALTER TABLE [dbo].[Bank] DROP CONSTRAINT [FK_Bank_Country]
GO
/****** Object:  View [dbo].[UserDetailsView]    Script Date: 12/10/2015 7:54:36 PM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[UserDetailsView]'))
DROP VIEW [dbo].[UserDetailsView]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserStatus]') AND type in (N'U'))
DROP TABLE [dbo].[UserStatus]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[ShipmentMode]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShipmentMode]') AND type in (N'U'))
DROP TABLE [dbo].[ShipmentMode]
GO
/****** Object:  Table [dbo].[ShipmentDetailCarton]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShipmentDetailCarton]') AND type in (N'U'))
DROP TABLE [dbo].[ShipmentDetailCarton]
GO
/****** Object:  Table [dbo].[ShipmentDetail]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShipmentDetail]') AND type in (N'U'))
DROP TABLE [dbo].[ShipmentDetail]
GO
/****** Object:  Table [dbo].[Shipment]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shipment]') AND type in (N'U'))
DROP TABLE [dbo].[Shipment]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Port]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Port]') AND type in (N'U'))
DROP TABLE [dbo].[Port]
GO
/****** Object:  Table [dbo].[OrderDeatilItem]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDeatilItem]') AND type in (N'U'))
DROP TABLE [dbo].[OrderDeatilItem]
GO
/****** Object:  Table [dbo].[InvoiceStatus]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceStatus]') AND type in (N'U'))
DROP TABLE [dbo].[InvoiceStatus]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND type in (N'U'))
DROP TABLE [dbo].[Invoice]
GO
/****** Object:  Table [dbo].[DistributorClientAddress]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DistributorClientAddress]') AND type in (N'U'))
DROP TABLE [dbo].[DistributorClientAddress]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
DROP TABLE [dbo].[Country]
GO
/****** Object:  Table [dbo].[Carton]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Carton]') AND type in (N'U'))
DROP TABLE [dbo].[Carton]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 12/10/2015 7:54:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bank]') AND type in (N'U'))
DROP TABLE [dbo].[Bank]
GO
