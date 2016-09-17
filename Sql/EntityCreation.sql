USE [IndicoPacking]
GO

/****** Object:  Table [dbo].[Shipment]   Script Date: 27-Aug-15 12:57:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Shipment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WeekNo] [int] NOT NULL,
	[WeekendDate] datetime2(7) NOT NULL,
	[IndicoWeeklyProductionCapacityID] [int] NULL,
 CONSTRAINT [PK_Shipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[ShipmentDeatil]    Script Date: 27-Aug-15 1:42:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShipmentDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Shipment] [int] NOT NULL,
	[IndicoDistributorClientAddress] [int] NOT NULL,
	[ShipTo] [nvarchar](255) NOT NULL,
	[Port] [nvarchar](255) NOT NULL,
	[ShipmentMode] [nvarchar](64) NULL,
	[PriceTerm] [nvarchar](64) NOT NULL,
	[ETD] [datetime2](7) NOT NULL,
	[Qty] [int] NOT NULL,
	[QuantityFilled] [int] NOT NULL,
	[QuantityYetToBeFilled] [int] NOT NULL,
	--[OrderStatus] [nvarchar](64) NOT NULL,
	[InvoiceNo] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_ShipmentDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ShipmentDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentDetail_Shipment] FOREIGN KEY([Shipment])
REFERENCES [dbo].[Shipment] ([ID])
GO

ALTER TABLE [dbo].[ShipmentDetail] CHECK CONSTRAINT [FK_ShipmentDetail_Shipment]
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[ShipmentOrder]   Script Date: 27-Aug-15 12:57:55 PM ******/

/* SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShipmentOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentDeatil] [int] NOT NULL,
	[IndicoOrderID] [int] NOT NULL,
 CONSTRAINT [PK_ShipmentOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ShipmentOrder]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentOrder_ShipmentDeatil] FOREIGN KEY([ShipmentDeatil])
REFERENCES [dbo].[ShipmentDetail] ([ID])
GO

ALTER TABLE [dbo].[ShipmentOrder] CHECK CONSTRAINT [FK_ShipmentOrder_ShipmentDeatil]
GO */

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[Carton]    Script Date: 6/10/2015 5:06:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Carton](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Qty] [int] NOT NULL,
	[Description] [nvarchar](128) NULL,
 CONSTRAINT [PK_Carton] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[ShipmentDetailCarton]    Script Date: 27-Aug-15 5:04:01 PM ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShipmentDetailCarton](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentDetail] [int] NOT NULL,
	[Carton] [int] NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_ShipmentDetailCarton] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ShipmentDetailCarton]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentDetailCarton_ShipmentDetail] FOREIGN KEY([ShipmentDetail])
REFERENCES [dbo].[ShipmentDetail] ([ID])
GO

ALTER TABLE [dbo].[ShipmentDetailCarton] CHECK CONSTRAINT [FK_ShipmentDetailCarton_ShipmentDetail]
GO

ALTER TABLE [dbo].[ShipmentDetailCarton]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentDetailCarton_Carton] FOREIGN KEY([Carton])
REFERENCES [dbo].[Carton] ([ID])
GO

ALTER TABLE [dbo].[ShipmentDetailCarton] CHECK CONSTRAINT [FK_ShipmentDetailCarton_Carton]
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[InvoiceStatus]    Script Date: 8/10/2015 12:37:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvoiceStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](64) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_InvoiceStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[Country]    Script Date: 12/10/2015 7:35:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Country](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Iso2] [nvarchar](2) NOT NULL,
	[Iso3] [nvarchar](3) NOT NULL,
	[IsoCountryNumber] [int] NOT NULL,
	[DialingPrefix] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[HasLocationData] [bit] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[Bank]    Script Date: 12/10/2015 6:02:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Bank](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AccountNo] [nvarchar](100) NOT NULL,
	[Branch] [nvarchar](255) NOT NULL,
	[Number] [nvarchar](32) NULL,
	[Address] [nvarchar](255) NULL,
	[City] [nvarchar](68) NULL,
	[State] [nvarchar](20) NULL,
	[Postcode] [nvarchar](20) NULL,
	[Country] [int] NULL,
	[SwiftCode] [nvarchar](100) NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Bank]  WITH CHECK ADD  CONSTRAINT [FK_Bank_Country] FOREIGN KEY([Country])
REFERENCES [dbo].[Country] ([ID])
GO

ALTER TABLE [dbo].[Bank] CHECK CONSTRAINT [FK_Bank_Country]
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[ShipmentMode]    Script Date: 12/10/2015 7:04:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShipmentMode](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[IndicoShipmentModeId] [int] NULL,
 CONSTRAINT [PK_ShipmentMode] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[DestinationPort]    Script Date: 12/10/2015 7:07:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Port](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[IndicoPortId] [int] NULL,
 CONSTRAINT [PK_Port] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[DistributorClientAddress]    Script Date: 12/10/2015 7:31:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DistributorClientAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Suburb] [nvarchar](255) NOT NULL,
	[PostCode] [nvarchar](255) NOT NULL,
	[Country] [int] NOT NULL,
	[ContactName] [nvarchar](255) NOT NULL,
	[ContactPhone] [nvarchar](255) NOT NULL,
	[CompanyName] [nvarchar](255) NOT NULL,
	[State] [nvarchar](255) NULL,
	[Port] [int] NULL,
	[EmailAddress] [nvarchar](128) NULL,
	[AddressType] [int] NULL,
	[IsAdelaideWarehouse] [bit] NOT NULL CONSTRAINT [DF_DistributorClientAddress_IsAdelaideWarehouse]  DEFAULT ((0)),
	[IndicoDistributorClientAddressId] [int] NULL,
 CONSTRAINT [PK_DistributorClientAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DistributorClientAddress]  WITH CHECK ADD  CONSTRAINT [FK_DistributorAddressr_Country] FOREIGN KEY([Country])
REFERENCES [dbo].[Country] ([ID])
GO

ALTER TABLE [dbo].[DistributorClientAddress] CHECK CONSTRAINT [FK_DistributorAddressr_Country]
GO

ALTER TABLE [dbo].[DistributorClientAddress]  WITH CHECK ADD  CONSTRAINT [FK_DistributorAddressr_Port] FOREIGN KEY([Port])
REFERENCES [dbo].[Port] ([ID])
GO

ALTER TABLE [dbo].[DistributorClientAddress] CHECK CONSTRAINT [FK_DistributorAddressr_Port]
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[Invoice]    Script Date: 8/10/2015 12:38:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentDetail] [int] NOT NULL,
	[FactoryInvoiceNumber] [nvarchar](64) NOT NULL,
	[FactoryInvoiceDate] [datetime2](7) NOT NULL,
	[AWBNumber] [nvarchar](255) NULL,
	[IndimanInvoiceNumber] [nvarchar](64) NULL,	
	[IndimanInvoiceDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[LastModifiedBy] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ShipmentDate] [datetime2](7) NOT NULL,
	[ShipTo] [int] NOT NULL,
	[BillTo] [int] NULL,
	[ShipmentMode] [int] NOT NULL,
	[Port] [int] NOT NULL,
	[Bank] [int] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_ShipmentDetail] FOREIGN KEY([ShipmentDetail])
REFERENCES [dbo].[ShipmentDetail] ([ID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_ShipmentDetail]
GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_ShipTo] FOREIGN KEY([ShipTo])
REFERENCES [dbo].[DistributorClientAddress] ([ID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_ShipTo]
GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_BillTo] FOREIGN KEY([BillTo])
REFERENCES [dbo].[DistributorClientAddress] ([ID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_BillTo]
GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_ShipmentMode] FOREIGN KEY([ShipmentMode])
REFERENCES [dbo].[ShipmentMode] ([ID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_ShipmentMode]
GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Port] FOREIGN KEY([Port])
REFERENCES [dbo].[Port] ([ID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Port]
GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[InvoiceStatus] ([ID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Status]
GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Bank] FOREIGN KEY([Bank])
REFERENCES [dbo].[Bank] ([ID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Bank]
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[OrderDeatilItem]    Script Date: 6/10/2015 5:05:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderDeatilItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentDeatil] [int] NOT NULL,
	[IndicoOrderID] [int] NOT NULL,
	[IndicoOrderDetailID] [int] NOT NULL,
	[ShipmentDetailCarton] [int] NULL,
	[OrderType] [nvarchar](64) NOT NULL,
	[Distributor] [nvarchar](128) NULL,
	[Client] [nvarchar](255) NOT NULL,
	[PurchaseOrder] [nvarchar](50) NULL,
	[VisualLayout] [nvarchar](64) NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[Pattern] [nvarchar](255) NOT NULL,
	[Fabric] [nvarchar](255) NOT NULL,
	[Material] [nvarchar](255) NULL,
	[Gender] [nvarchar](64) NULL,
	[AgeGroup] [nvarchar](64) NULL,
	[SleeveShape] [nvarchar](64) NULL,
	[SleeveLength] [nvarchar](64) NULL,
	[ItemSubGroup] [nvarchar](64) NOT NULL,
	[PaymentMethod] [nvarchar](64) NULL,
	[SizeDesc] [nvarchar](255) NOT NULL,
	[SizeQty] [int] NULL,
	[SizeSrno] [int] NULL,
	[Status] [nvarchar](64) NOT NULL,
	[IsPolybagScanned] [bit] NOT NULL CONSTRAINT [DF_OrderDeatilItem_IsPolybagScanned]  DEFAULT ((0)),
	[PrintedCount] [int] NULL,
	[PatternImage] [nvarchar](255) NULL,
	[VLImage] [nvarchar](255) NULL,
	[PatternNumber] [nvarchar](64) NOT NULL,
	[DateScanned] [datetime] NULL,
	[Invoice] [int] NULL,
	[FactoryPrice] [decimal](8, 2) NULL,
	[IndimanPrice] [decimal](8, 2) NULL,	
	[OtherCharges] [decimal](8, 2) NULL,
	[Notes] [nvarchar](255) NULL,
	[PatternInvoiceNotes] [nvarchar](255) NULL,
	[ProductNotes] [nvarchar](255) NULL,	
	[JKFOBCostSheetPrice] [decimal](8, 2) NULL,
	[IndimanCIFCostSheetPrice] [decimal](8, 2) NULL,
	[HSCode] [nvarchar](64) NOT NULL,
	[ItemName] [nvarchar](64) NOT NULL,
	[PurchaseOrderNo] [nvarchar](50) NULL
 CONSTRAINT [PK_OrderDeatilItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrderDeatilItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderDeatilItem_ShipmentDeatil] FOREIGN KEY([ShipmentDeatil])
REFERENCES [dbo].[ShipmentDetail] ([ID])
GO

ALTER TABLE [dbo].[OrderDeatilItem] CHECK CONSTRAINT [FK_OrderDeatilItem_ShipmentDeatil]
GO

ALTER TABLE [dbo].[OrderDeatilItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderDeatilItem_ShipmentDetailCarton] FOREIGN KEY([ShipmentDetailCarton])
REFERENCES [dbo].[ShipmentDetailCarton] ([ID])
GO

ALTER TABLE [dbo].[OrderDeatilItem] CHECK CONSTRAINT [FK_OrderDeatilItem_ShipmentDetailCarton]
GO

ALTER TABLE [dbo].[OrderDeatilItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderDeatilItem_Invoice] FOREIGN KEY([Invoice])
REFERENCES [dbo].[Invoice] ([ID])
GO

ALTER TABLE [dbo].[OrderDeatilItem] CHECK CONSTRAINT [FK_OrderDeatilItem_Invoice]
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[UserStatus]    Script Date: 6/10/2015 10:45:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](4) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[Role]    Script Date: 6/10/2015 10:46:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](4) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[User]    Script Date: 6/10/2015 10:41:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[Username] [nvarchar](64) NOT NULL,
	[Password] [varchar](128) NOT NULL,
	[PasswordSalt] [varchar](128) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[EmailAddress] [nvarchar](128) NULL,
	[MobileTelephoneNumber] [nvarchar](20) NULL,
	[OfficeTelephoneNumber] [nvarchar](20) NULL,
	[DateLastLogin] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[GenderMale] [bit] NOT NULL CONSTRAINT [DF_User_GenderMale]  DEFAULT ((1)),
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[UserStatus] ([ID])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserStatus]
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Role] ([ID])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO

ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_LastModifiedBy]
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

/****** Object:  Table [dbo].[InvoiceOrderDeatilItem]    Script Date: 8/10/2015 12:38:48 AM ******/
/*SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvoiceOrderDeatilItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Invoice] [int] NOT NULL,
	[OrderDeatilItem] [int] NOT NULL,
	[FactoryPrice] [decimal](8, 2) NULL,
	[IndimanPrice] [decimal](8, 2) NULL,
 CONSTRAINT [PK_InvoiceOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InvoiceOrder]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceOrder_Invoice] FOREIGN KEY([Invoice])
REFERENCES [dbo].[Invoice] ([ID])
GO

ALTER TABLE [dbo].[InvoiceOrder] CHECK CONSTRAINT [FK_InvoiceOrder_Invoice]
GO

ALTER TABLE [dbo].[InvoiceOrder]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceOrder_OrderDeatilItem] FOREIGN KEY([OrderDeatilItem])
REFERENCES [dbo].[OrderDeatilItem] ([ID])
GO

ALTER TABLE [dbo].[InvoiceOrder] CHECK CONSTRAINT [FK_InvoiceOrder_OrderDeatilItem]
GO */

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**