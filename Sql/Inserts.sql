Use [IndicoPacking]
GO

INSERT INTO [dbo].[Carton]
           ([Name]
           ,[Qty]
		   ,[Description])
     VALUES
           ('60X40X17 CM'
           ,50
		   ,'Small')
GO

INSERT INTO [dbo].[Carton]
           ([Name]
           ,[Qty]
		   ,[Description])
     VALUES
           ('60X40X28 CM'
           ,60
		   ,'Medium')
GO

INSERT INTO [dbo].[Carton]
           ([Name]
           ,[Qty]
		   ,[Description])
     VALUES
           ('60X40X40 CM'
           ,70
		   ,'Large')
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

INSERT INTO [dbo].[UserStatus]([Key],[Name])
     VALUES ('A','Active')
INSERT INTO [dbo].[UserStatus]([Key],[Name])
     VALUES ('I','Inactive')
INSERT INTO [dbo].[UserStatus]([Key],[Name])
     VALUES ('D','Delete')
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

INSERT INTO [dbo].[Role]
           ([Key]
           ,[Name])
     VALUES
           ('AAD',
           'Application Administrator')
GO

INSERT INTO [dbo].[Role]
           ([Key]
           ,[Name])
     VALUES
           ('IAD',
           'Indiman Administrator')
GO

INSERT INTO [dbo].[Role]
           ([Key]
           ,[Name])
     VALUES
           ('JKA',
           'JK Administrator')
GO

INSERT INTO [dbo].[Role]
           ([Key]
           ,[Name])
     VALUES
           ('FOP',
           'Filling Operator')
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

INSERT INTO [dbo].[User]
           ([Status]
           ,[Role]
           ,[Username]
           ,[Password]
           ,[PasswordSalt]
           ,[Name]
           ,[EmailAddress]
           ,[MobileTelephoneNumber]
           ,[OfficeTelephoneNumber]
           ,[DateLastLogin]
           ,[CreatedDate]
           ,[GenderMale])
     VALUES
           (1
           ,1
           ,'Admin'
           ,'s9bCEbjpidn1GRFrk/iHL/uTgy8=' -- IndicoPac
           ,'NgAzADUAOAAyADAAOAA1ADgAOAAyADgAMAAzADcANQA1ADEA'
           ,'Application Administrator'
           ,'admin@Indico.net'
           ,null
           ,null
           ,null
           ,GETDATE()
           ,1)
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

INSERT INTO [dbo].[InvoiceStatus]
           ([Key]
           ,[Name])
     VALUES
           ('PS'
           ,'Pre-Shipped')
GO

INSERT INTO [dbo].[InvoiceStatus]
           ([Key]
           ,[Name])
     VALUES
           ('S'
           ,'Shipped')
GO

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**
-- Country
INSERT INTO [IndicoPacking].[dbo].[Country]
           ([Iso2]
           ,[Iso3]
           ,[IsoCountryNumber]
           ,[DialingPrefix]
           ,[Name]
           ,[ShortName]
           ,[HasLocationData])
SELECT     [Iso2]
		  ,[Iso3]
		  ,[IsoCountryNumber]
		  ,[DialingPrefix]
		  ,[Name]
		  ,[ShortName]
		  ,[HasLocationData]
FROM [Indico].[dbo].[Country]

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

-- Port
INSERT INTO [IndicoPacking].[dbo].[Port]
           ([Name]
           ,[Description]
           ,[IndicoPortId])
SELECT     [Name]
		   ,[Description]
		   ,[ID]
FROM [Indico].[dbo].[DestinationPort]

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

-- Payment method
INSERT INTO [IndicoPacking].[dbo].[ShipmentMode]
           ([Name]
           ,[Description]
           ,[IndicoShipmentModeId])
SELECT     [Name]
           ,[Description]
		   ,[ID]
FROM [Indico].[dbo].[ShipmentMode]

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

INSERT INTO [IndicoPacking].[dbo].[Bank]
           ([Name]
           ,[AccountNo]
           ,[Branch]
           ,[Number]
           ,[Address]
           ,[City]
           ,[State]
           ,[Postcode]
           ,[Country]
           ,[SwiftCode])
SELECT	   [Name]
		  ,[AccountNo]
		  ,[Branch]
		  ,[Number]
		  ,[Address]
		  ,[City]
		  ,[State]
		  ,[Postcode]
		  ,[Country]
		  ,[SwiftCode]
FROM [Indico].[dbo].[Bank]

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**

INSERT INTO [IndicoPacking].[dbo].[DistributorClientAddress]
           ([Address]
           ,[Suburb]
           ,[PostCode]
           ,[Country]
           ,[ContactName]
           ,[ContactPhone]
           ,[CompanyName]
           ,[State]
           ,[Port]
           ,[EmailAddress]
           ,[AddressType]
           ,[IsAdelaideWarehouse]
           ,[IndicoDistributorClientAddressId])
SELECT	   dca.[Address]
		  ,dca.[Suburb]
		  ,dca.[PostCode]
		  ,dca.[Country]
		  ,dca.[ContactName]
		  ,dca.[ContactPhone]
		  ,dca.[CompanyName]
		  ,dca.[State]
		  ,dca.[Port]
		  ,dca.[EmailAddress]
		  ,dca.[AddressType]
		  ,dca.[IsAdelaideWarehouse]
		  ,dca.[ID]
FROM [Indico].[dbo].[DistributorClientAddress] dca

--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**--**
