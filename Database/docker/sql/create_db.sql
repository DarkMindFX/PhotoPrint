USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [ppt_svc_api]    Script Date: 11/6/2021 11:17:38 AM ******/
CREATE LOGIN [ppt_svc_api] WITH PASSWORD=N'PPTServiceApi2021!', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [ppt_test_account]    Script Date: 11/6/2021 11:18:44 AM ******/
CREATE LOGIN [ppt_test_account] WITH PASSWORD=N'PPTTestAccount2021', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE master
GO


/**************** Creating DB ****************************/
DECLARE @dbname nvarchar(128)
SET @dbname = N'PhotoPrint'

IF (NOT EXISTS (SELECT name 
	FROM master.sys.databases 
	WHERE ('[' + [name] + ']' = @dbname 
	OR [name] = @dbname)))
BEGIN
	CREATE DATABASE PhotoPrint
END
GO



GO

/******************* Creating Users ***********************************/
USE [PhotoPrint]
GO
/****** Object:  User [ppt_svc_api]    Script Date: 11/6/2021 11:14:11 AM ******/
CREATE USER [ppt_svc_api] FOR LOGIN [ppt_svc_api] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ppt_svc_api]
GO

USE [PhotoPrint]
GO
/****** Object:  User [ppt_test_account]    Script Date: 11/6/2021 11:14:12 AM ******/
CREATE USER [ppt_test_account] FOR LOGIN [ppt_test_account] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ppt_test_account]
GO



/**************** Creating Tables ****************************/
USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserStatus] ADD  CONSTRAINT [DF_UserStatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserTypeName] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserType] ADD  CONSTRAINT [DF_UserType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderStatusName] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderStatus] ADD  CONSTRAINT [DF_OrderStatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](50) NOT NULL,
	[UnitAbbr] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](250) NOT NULL,
	[PwdHash] [nvarchar](250) NOT NULL,
	[Salt] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FriendlyName] [nvarchar](50) NULL,
	[UserStatusID] [bigint] NOT NULL,
	[UserTypeID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User]
GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_UserStatus] FOREIGN KEY([UserStatusID])
REFERENCES [dbo].[UserStatus] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserStatus]
GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserType] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserType]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[AddressType]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressTypeName] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AddressType] ADD  CONSTRAINT [DF_AddressType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[ParentID] [bigint] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_ImageCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Category]  WITH NOCHECK ADD  CONSTRAINT [FK_Category_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_CreatedByUser]
GO
ALTER TABLE [dbo].[Category]  WITH NOCHECK ADD  CONSTRAINT [FK_Category_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_ModifiedByUser]
GO
ALTER TABLE [dbo].[Category]  WITH NOCHECK ADD  CONSTRAINT [FK_ImageCategory_ImageCategory] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_ImageCategory_ImageCategory]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ContactTypeName] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
	[ISO] [nvarchar](5) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[RegionName] [nvarchar](50) NOT NULL,
	[CountryID] [bigint] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Region] ADD  CONSTRAINT [DF_Region_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Region]  WITH CHECK ADD  CONSTRAINT [FK_Region_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Region] CHECK CONSTRAINT [FK_Region_Country]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[City]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](250) NOT NULL,
	[RegionID] [bigint] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_Region] FOREIGN KEY([RegionID])
REFERENCES [dbo].[Region] ([ID])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_Region]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressTypeID] [bigint] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[CityID] [bigint] NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[BuildingNo] [nvarchar](50) NOT NULL,
	[ApartmentNo] [nvarchar](50) NULL,
	[Comment] [nvarchar](1000) NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_AddressType] FOREIGN KEY([AddressTypeID])
REFERENCES [dbo].[AddressType] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_AddressType]
GO
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_City]
GO
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_UserCreated] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_UserCreated]
GO
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_UserModified] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_UserModified]

GO
USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ContactTypeID] [bigint] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Comment] [nvarchar](250) NULL,
	[Value] [nvarchar](1000) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_IsPrimary]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Contact]  WITH NOCHECK ADD  CONSTRAINT [FK_Contact_ContactType] FOREIGN KEY([ContactTypeID])
REFERENCES [dbo].[ContactType] ([ID])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_ContactType]
GO
ALTER TABLE [dbo].[Contact]  WITH NOCHECK ADD  CONSTRAINT [FK_Contact_UserCreated] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_UserCreated]
GO
ALTER TABLE [dbo].[Contact]  WITH NOCHECK ADD  CONSTRAINT [FK_Contact_UserModified] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_UserModified]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ISO] [nvarchar](5) NOT NULL,
	[CurrencyName] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Currency] ADD  CONSTRAINT [DF_Currency_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[DeliveryService]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryService](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeliveryServiceName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_DeliveryType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeliveryService] ADD  CONSTRAINT [DF_DeliveryType_IsEnabled]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DeliveryService]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryService_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[DeliveryService] CHECK CONSTRAINT [FK_DeliveryService_CreatedByUser]
GO
ALTER TABLE [dbo].[DeliveryService]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryService_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[DeliveryService] CHECK CONSTRAINT [FK_DeliveryService_ModifiedByUser]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[DeliveryServiceCity]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryServiceCity](
	[DeliveryServiceID] [bigint] NOT NULL,
	[CityID] [bigint] NOT NULL,
 CONSTRAINT [PK_DeliveryServiceCity_1] PRIMARY KEY CLUSTERED 
(
	[DeliveryServiceID] ASC,
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeliveryServiceCity]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryServiceCity_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeliveryServiceCity] CHECK CONSTRAINT [FK_DeliveryServiceCity_City]
GO
ALTER TABLE [dbo].[DeliveryServiceCity]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryServiceCity_DeliveryService] FOREIGN KEY([DeliveryServiceID])
REFERENCES [dbo].[DeliveryService] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeliveryServiceCity] CHECK CONSTRAINT [FK_DeliveryServiceCity_DeliveryService]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[FrameType]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FrameType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FrameTypeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[ThumbnailUrl] [nvarchar](1000) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_FrameType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FrameType] ADD  CONSTRAINT [DF_FrameType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FrameType]  WITH NOCHECK ADD  CONSTRAINT [FK_FrameType_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[FrameType] CHECK CONSTRAINT [FK_FrameType_CreatedByUser]
GO
ALTER TABLE [dbo].[FrameType]  WITH NOCHECK ADD  CONSTRAINT [FK_FrameType_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[FrameType] CHECK CONSTRAINT [FK_FrameType_ModifiedByUser]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[OriginUrl] [nvarchar](3000) NOT NULL,
	[MaxWidth] [int] NULL,
	[MaxHeight] [int] NULL,
	[PriceAmount] [decimal](18, 2) NULL,
	[PriceCurrencyID] [bigint] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Image]  WITH NOCHECK ADD  CONSTRAINT [FK_Image_Currency] FOREIGN KEY([PriceCurrencyID])
REFERENCES [dbo].[Currency] ([ID])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_Currency]
GO
ALTER TABLE [dbo].[Image]  WITH NOCHECK ADD  CONSTRAINT [FK_Image_UserCreated] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_UserCreated]
GO
ALTER TABLE [dbo].[Image]  WITH NOCHECK ADD  CONSTRAINT [FK_Image_UserModified] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_UserModified]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[ImageCategory]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageCategory](
	[ImageID] [bigint] NOT NULL,
	[CategoryID] [bigint] NOT NULL,
 CONSTRAINT [PK_ImageCategory_1] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC,
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ImageCategory]  WITH NOCHECK ADD  CONSTRAINT [FK_ImageCategory_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImageCategory] CHECK CONSTRAINT [FK_ImageCategory_Category]
GO
ALTER TABLE [dbo].[ImageCategory]  WITH NOCHECK ADD  CONSTRAINT [FK_ImageCategory_Image] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Image] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImageCategory] CHECK CONSTRAINT [FK_ImageCategory_Image]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[ImageRelated]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageRelated](
	[ImageID] [bigint] NOT NULL,
	[RelatedImageID] [bigint] NOT NULL,
 CONSTRAINT [PK_ImageRelated_1] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC,
	[RelatedImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ImageRelated]  WITH NOCHECK ADD  CONSTRAINT [FK_ImageRelated_RelatedImage] FOREIGN KEY([RelatedImageID])
REFERENCES [dbo].[Image] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImageRelated] CHECK CONSTRAINT [FK_ImageRelated_RelatedImage]
GO
ALTER TABLE [dbo].[ImageRelated]  WITH NOCHECK ADD  CONSTRAINT [FK_ImageRelated_RootImage] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[ImageRelated] CHECK CONSTRAINT [FK_ImageRelated_RootImage]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[ImageThumbnail]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageThumbnail](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[Order] [int] NULL,
	[ImageID] [bigint] NOT NULL,
 CONSTRAINT [PK_ImageThumbnail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ImageThumbnail]  WITH NOCHECK ADD  CONSTRAINT [FK_ImageThumbnail_Image] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Image] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImageThumbnail] CHECK CONSTRAINT [FK_ImageThumbnail_Image]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Mat]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mat](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MatName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[ThumbnailUrl] [nvarchar](1000) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_Mat] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Mat] ADD  CONSTRAINT [DF_Mat_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Mat]  WITH NOCHECK ADD  CONSTRAINT [FK_Mat_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Mat] CHECK CONSTRAINT [FK_Mat_CreatedByUser]
GO
ALTER TABLE [dbo].[Mat]  WITH NOCHECK ADD  CONSTRAINT [FK_Mat_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Mat] CHECK CONSTRAINT [FK_Mat_ModifiedByUser]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[MaterialType]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MaterialTypeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[ThumbnailUrl] [nvarchar](1000) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_MaterialType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MaterialType] ADD  CONSTRAINT [DF_MaterialType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MaterialType]  WITH NOCHECK ADD  CONSTRAINT [FK_MaterialType_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[MaterialType] CHECK CONSTRAINT [FK_MaterialType_CreatedByUser]
GO
ALTER TABLE [dbo].[MaterialType]  WITH NOCHECK ADD  CONSTRAINT [FK_MaterialType_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[MaterialType] CHECK CONSTRAINT [FK_MaterialType_ModifiedByUser]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[MountingType]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MountingType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MountingTypeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[ThumbnailUrl] [nvarchar](1000) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_MountingType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MountingType] ADD  CONSTRAINT [DF_MountingType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ManagerID] [bigint] NULL,
	[UserID] [bigint] NOT NULL,
	[ContactID] [bigint] NOT NULL,
	[DeliveryAddressID] [bigint] NOT NULL,
	[DeliveryServiceID] [bigint] NOT NULL,
	[Comments] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Order]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_AddressDelivery] FOREIGN KEY([DeliveryAddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_AddressDelivery]
GO
ALTER TABLE [dbo].[Order]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contact] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Contact]
GO
ALTER TABLE [dbo].[Order]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_DeliveryService] FOREIGN KEY([DeliveryServiceID])
REFERENCES [dbo].[DeliveryService] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_DeliveryService]
GO
ALTER TABLE [dbo].[Order]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_ManagerUser] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_ManagerUser]
GO
ALTER TABLE [dbo].[Order]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[Order]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_UserCreated] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_UserCreated]
GO
ALTER TABLE [dbo].[Order]  WITH NOCHECK ADD  CONSTRAINT [FK_Order_UserModified] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_UserModified]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SizeName] [nvarchar](50) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Size] ADD  CONSTRAINT [DF_Size_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Size]  WITH NOCHECK ADD  CONSTRAINT [FK_Size_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Size] CHECK CONSTRAINT [FK_Size_CreatedByUser]
GO
ALTER TABLE [dbo].[Size]  WITH NOCHECK ADD  CONSTRAINT [FK_Size_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Size] CHECK CONSTRAINT [FK_Size_ModifiedByUser]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[PrintingHouse]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintingHouse](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_PrintingHouse] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrintingHouse] ADD  CONSTRAINT [DF_PrintingHouse_IsEnabled]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PrintingHouse]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouse_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouse] CHECK CONSTRAINT [FK_PrintingHouse_CreatedByUser]
GO
ALTER TABLE [dbo].[PrintingHouse]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouse_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouse] CHECK CONSTRAINT [FK_PrintingHouse_ModifiedByUser]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PaymentMethod] ADD  CONSTRAINT [DF_PaymentMethod_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[ImageID] [bigint] NOT NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[SizeID] [bigint] NULL,
	[FrameTypeID] [bigint] NOT NULL,
	[FrameSizeID] [bigint] NULL,
	[MatID] [bigint] NOT NULL,
	[MaterialTypeID] [bigint] NOT NULL,
	[MountingTypeID] [bigint] NOT NULL,
	[ItemCount] [int] NOT NULL,
	[PriceAmountPerItem] [decimal](18, 2) NOT NULL,
	[PriceCurrencyID] [bigint] NOT NULL,
	[Comments] [nvarchar](1000) NOT NULL,
	[PrintingHouseID] [bigint] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItem] ADD  CONSTRAINT [DF_OrderItem_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_CreatedByUser]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_Currency] FOREIGN KEY([PriceCurrencyID])
REFERENCES [dbo].[Currency] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Currency]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_FrameSize] FOREIGN KEY([FrameSizeID])
REFERENCES [dbo].[Size] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_FrameSize]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_FrameType] FOREIGN KEY([FrameTypeID])
REFERENCES [dbo].[FrameType] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_FrameType]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_Image] FOREIGN KEY([ImageID])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Image]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_Mat] FOREIGN KEY([MatID])
REFERENCES [dbo].[Mat] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Mat]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_MaterialType] FOREIGN KEY([MaterialTypeID])
REFERENCES [dbo].[MaterialType] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_MaterialType]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_ModifiedByUser]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_MountingType] FOREIGN KEY([MountingTypeID])
REFERENCES [dbo].[MountingType] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_MountingType]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_PrintingHouse] FOREIGN KEY([PrintingHouseID])
REFERENCES [dbo].[PrintingHouse] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_PrintingHouse]
GO
ALTER TABLE [dbo].[OrderItem]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderItem_Size] FOREIGN KEY([SizeID])
REFERENCES [dbo].[Size] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Size]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderPaymentDetails]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderPaymentDetails](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[PaymentMethodID] [bigint] NOT NULL,
	[PaymentTransUID] [nvarchar](250) NULL,
	[PaymentDateTime] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_OrderPaymentDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderPaymentDetails] ADD  CONSTRAINT [DF_OrderPaymentDetails_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[OrderPaymentDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderPaymentDetails_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[OrderPaymentDetails] CHECK CONSTRAINT [FK_OrderPaymentDetails_CreatedByUser]
GO
ALTER TABLE [dbo].[OrderPaymentDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderPaymentDetails_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[OrderPaymentDetails] CHECK CONSTRAINT [FK_OrderPaymentDetails_ModifiedByUser]
GO
ALTER TABLE [dbo].[OrderPaymentDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderPaymentDetails_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderPaymentDetails] CHECK CONSTRAINT [FK_OrderPaymentDetails_Order]
GO
ALTER TABLE [dbo].[OrderPaymentDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderPaymentDetails_PaymentMethod] FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[PaymentMethod] ([ID])
GO
ALTER TABLE [dbo].[OrderPaymentDetails] CHECK CONSTRAINT [FK_OrderPaymentDetails_PaymentMethod]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderStatusFlow]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatusFlow](
	[FromStatusID] [bigint] NOT NULL,
	[ToStatusID] [bigint] NOT NULL,
 CONSTRAINT [PK_OrderStatusFlow] PRIMARY KEY CLUSTERED 
(
	[FromStatusID] ASC,
	[ToStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderStatusFlow]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderStatusFlow_StatusFrom] FOREIGN KEY([FromStatusID])
REFERENCES [dbo].[OrderStatus] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderStatusFlow] CHECK CONSTRAINT [FK_OrderStatusFlow_StatusFrom]
GO
ALTER TABLE [dbo].[OrderStatusFlow]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderStatusFlow_StatusTo] FOREIGN KEY([ToStatusID])
REFERENCES [dbo].[OrderStatus] ([ID])
GO
ALTER TABLE [dbo].[OrderStatusFlow] CHECK CONSTRAINT [FK_OrderStatusFlow_StatusTo]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderTracking]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTracking](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[OrderStatusID] [bigint] NOT NULL,
	[SetDate] [datetime] NOT NULL,
	[SetByID] [bigint] NOT NULL,
	[Comment] [nvarchar](1000) NULL,
 CONSTRAINT [PK_OrderTracking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderTracking]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderTracking_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderTracking] CHECK CONSTRAINT [FK_OrderTracking_Order]
GO
ALTER TABLE [dbo].[OrderTracking]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderTracking_OrderStatus] FOREIGN KEY([OrderStatusID])
REFERENCES [dbo].[OrderStatus] ([ID])
GO
ALTER TABLE [dbo].[OrderTracking] CHECK CONSTRAINT [FK_OrderTracking_OrderStatus]
GO
ALTER TABLE [dbo].[OrderTracking]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderTracking_User] FOREIGN KEY([SetByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[OrderTracking] CHECK CONSTRAINT [FK_OrderTracking_User]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[PrintingHouseAddress]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintingHouseAddress](
	[PrintingHouseID] [bigint] NOT NULL,
	[AddressID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_PrintingHouseAddress_1] PRIMARY KEY CLUSTERED 
(
	[PrintingHouseID] ASC,
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrintingHouseAddress] ADD  CONSTRAINT [DF_PrintingHouseAddress_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[PrintingHouseAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouseAddress_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouseAddress] CHECK CONSTRAINT [FK_PrintingHouseAddress_Address]
GO
ALTER TABLE [dbo].[PrintingHouseAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouseAddress_PrintingHouse] FOREIGN KEY([PrintingHouseID])
REFERENCES [dbo].[PrintingHouse] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouseAddress] CHECK CONSTRAINT [FK_PrintingHouseAddress_PrintingHouse]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[PrintingHouseContact]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintingHouseContact](
	[PrintingHouseID] [bigint] NOT NULL,
	[ContactID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_PrintingHouseContact_1] PRIMARY KEY CLUSTERED 
(
	[PrintingHouseID] ASC,
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrintingHouseContact] ADD  CONSTRAINT [DF_PrintingHouseContact_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[PrintingHouseContact]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouseContact_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contact] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouseContact] CHECK CONSTRAINT [FK_PrintingHouseContact_Contact]
GO
ALTER TABLE [dbo].[PrintingHouseContact]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouseContact_PrintingHouseContact] FOREIGN KEY([PrintingHouseID])
REFERENCES [dbo].[PrintingHouse] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PrintingHouseContact] CHECK CONSTRAINT [FK_PrintingHouseContact_PrintingHouseContact]
GO




USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserAddress]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAddress](
	[UserID] [bigint] NOT NULL,
	[AddressID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_UserAddress] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserAddress] ADD  CONSTRAINT [DF_UserAddress_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[UserAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_UserAddress_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAddress] CHECK CONSTRAINT [FK_UserAddress_Address]
GO
ALTER TABLE [dbo].[UserAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_UserAddress_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAddress] CHECK CONSTRAINT [FK_UserAddress_User]
GO
USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserConfirmation]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserConfirmation](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[ConfirmationCode] [nvarchar](50) NOT NULL,
	[Comfirmed] [bit] NOT NULL,
	[ExpiresDate] [datetime] NOT NULL,
	[ConfirmationDate] [datetime] NULL,
 CONSTRAINT [PK_UserConfirmation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserConfirmation] ADD  CONSTRAINT [DF_UserConfirmation_Comfirmed]  DEFAULT ((0)) FOR [Comfirmed]
GO
ALTER TABLE [dbo].[UserConfirmation]  WITH NOCHECK ADD  CONSTRAINT [FK_UserConfirmation_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserConfirmation] CHECK CONSTRAINT [FK_UserConfirmation_User]
GO

USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserContact]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserContact](
	[UserID] [bigint] NOT NULL,
	[ContactID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_UserContact] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserContact] ADD  CONSTRAINT [DF_UserContact_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[UserContact]  WITH NOCHECK ADD  CONSTRAINT [FK_UserContact_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contact] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserContact] CHECK CONSTRAINT [FK_UserContact_Contact]
GO
ALTER TABLE [dbo].[UserContact]  WITH NOCHECK ADD  CONSTRAINT [FK_UserContact_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserContact] CHECK CONSTRAINT [FK_UserContact_User]
GO

/************************ Create Views ******************************************/

if exists(select 1 from sys.views where name='v_Orders' and type='v')
drop view dbo.v_Orders;
go

CREATE VIEW dbo.v_Orders
AS
SELECT
	o.ID								as [OrderID],
	manager.[Login]						as [Manager],
	client.[Login]						as [Client],
	status.[OrderStatusName]			as [Order Status],
	ds.[DeliveryServiceName]			as [Delivery Service],
	pm.[Name]							as [Payment Method],
	opd.[PaymentTransUID]				as [Transaction ID],
	o.*
	
FROM
	dbo.[Order] o
INNER JOIN dbo.[User] client		ON client.ID = o.UserID
LEFT JOIN dbo.[User] manager		ON manager.ID = o.ManagerID
INNER JOIN 
(
SELECT 
	tracking.OrderID,
	tracking.OrderStatusID,
	tracking.SetDate
FROM
	dbo.[OrderTracking] tracking
WHERE tracking.SetDate IN (SELECT MAX(SetDate) FROM dbo.[OrderTracking] GROUP BY OrderID)
)
ot	ON ot.OrderID = o.ID
INNER JOIN dbo.[OrderStatus] status ON status.ID = ot.OrderStatusID
INNER JOIN dbo.[DeliveryService] ds ON ds.ID = o.DeliveryServiceID
INNER JOIN dbo.[OrderPaymentDetails] opd ON opd.OrderID = o.ID
INNER JOIN dbo.[PaymentMethod] pm		ON pm.ID = opd.PaymentMethodID
GO


/************************ Create Functions ******************************************/
USE [PhotoPrint]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetUserStatusIDByName]    Script Date: 10/16/2021 9:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetUserStatusIDByName] 
(
	@StatusName NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	
	SELECT @Result = [ID] FROM dbo.[UserStatus] WHERE StatusName = @StatusName


	RETURN @Result
END
GO
USE [PhotoPrint]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetUserTypeIDByName]    Script Date: 10/16/2021 9:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[fn_GetUserTypeIDByName] 
(
	@UserTypeName NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	
	SELECT @Result = [ID] FROM dbo.[UserType] WHERE UserTypeName = @UserTypeName


	RETURN @Result
END
GO

/************************ Create StorProcs ******************************************/


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_AddressType_Populate'))
   DROP PROC dbo.p_AddressType_Populate
GO

CREATE PROCEDURE dbo.p_AddressType_Populate 
	
AS
BEGIN

	DECLARE @tblAddressType AS TABLE (
		[ID] [bigint] NOT NULL,
		[AddressTypeName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblAddressType
	SELECT 1, 'Main'	UNION 
	SELECT 2, 'Billing'		UNION
	SELECT 3, 'Delivery'	UNION
	SELECT 4, 'Office'		UNION
	SELECT 5, 'Production'	UNION
	SELECT 6, 'Warehouse'	

	SET IDENTITY_INSERT dbo.AddressType ON;

	MERGE dbo.AddressType AS t
	USING @tblAddressType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[AddressTypeName] = s.[AddressTypeName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[AddressTypeName]) VALUES (s.[ID], s.[AddressTypeName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.AddressType OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_City_Populate'))
   DROP PROC dbo.p_City_Populate
GO

CREATE PROCEDURE dbo.p_City_Populate 
	
AS
BEGIN

	DECLARE @tblCity AS TABLE (
		[ID] [bigint] NOT NULL,
		[CityName] [nvarchar](50) NOT NULL,
		[RegionID] [bigint] NOT NULL
	)

	INSERT INTO @tblCity
	-- UKRAINE Kharkivska obl
	SELECT 1, 'Kharkiv', 1	UNION 
	SELECT 2, 'Chuguev', 1	UNION
	-- UKRAINE Kyivska obl
	SELECT 3, 'Kyiv', 2		UNION
	SELECT 4, 'Boryspil', 2		UNION
	-- UKRAINE Dniprovska obl
	SELECT 5, 'Dnipro', 3 UNION
	-- UKRAINE Rivnenska obl
	SELECT 6, 'Rivne', 4 UNION
	-- UKRAINE Zhytomyrska obl
	SELECT 7, 'Zhytomyr', 5 UNION
	-- POLAND Pomorskie
	SELECT 8, 'Gdansk', 8 UNION
	SELECT 9, 'Gdynia', 8 UNION
	-- POLAND MAzowieckie
	SELECT 10, 'Warszawa', 9

	SET IDENTITY_INSERT dbo.City ON;

	MERGE dbo.City AS t
	USING @tblCity AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[CityName] = s.[CityName],
			t.[RegionID] = s.[RegionID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[CityName], [RegionID]) VALUES (s.[ID], s.[CityName], s.[RegionID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.City OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_ContactType_Populate'))
   DROP PROC dbo.p_ContactType_Populate
GO

CREATE PROCEDURE dbo.p_ContactType_Populate 
	
AS
BEGIN

	DECLARE @tblContactType AS TABLE (
		[ID] [bigint] NOT NULL,
		[ContactTypeName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblContactType
	SELECT 1, 'Email'	UNION 
	SELECT 2, 'Phone'		UNION
	SELECT 3, 'WhatsApp'		UNION
	SELECT 4, 'Viber'		UNION
	SELECT 5, 'Skype'	UNION
	SELECT 6, 'Telegram'

	SET IDENTITY_INSERT dbo.ContactType ON;

	MERGE dbo.ContactType AS t
	USING @tblContactType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[ContactTypeName] = s.[ContactTypeName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[ContactTypeName]) VALUES (s.[ID], s.[ContactTypeName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.ContactType OFF;
	
	SET NOCOUNT ON;
END
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_Country_Populate'))
   DROP PROC dbo.p_Country_Populate
GO

CREATE PROCEDURE dbo.p_Country_Populate 
	
AS
BEGIN

	DECLARE @tblCountry AS TABLE (
		[ID] [bigint] NOT NULL,
		[ISO] [nvarchar](5) NOT NULL,
		[CountryName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblCountry
	SELECT 1, 'AF', 'Afghanistan' UNION
	SELECT 2, 'AL', 'Albania' UNION
	SELECT 3, 'DZ', 'Algeria' UNION
	SELECT 4, 'AS', 'American Samoa' UNION
	SELECT 5, 'AD', 'Andorra' UNION
	SELECT 6, 'AO', 'Angola' UNION
	SELECT 7, 'AI', 'Anguilla' UNION
	SELECT 8, 'AQ', 'Antarctica' UNION
	SELECT 9, 'AG', 'Antigua and Barbuda' UNION
	SELECT 10, 'AR', 'Argentina' UNION
	SELECT 11, 'AM', 'Armenia' UNION
	SELECT 12, 'AW', 'Aruba' UNION
	SELECT 13, 'AU', 'Australia' UNION
	SELECT 14, 'AT', 'Austria' UNION
	SELECT 15, 'AZ', 'Azerbaijan' UNION
	SELECT 16, 'BS', 'Bahamas' UNION
	SELECT 17, 'BH', 'Bahrain' UNION
	SELECT 18, 'BD', 'Bangladesh' UNION
	SELECT 19, 'BB', 'Barbados' UNION
	SELECT 20, 'BY', 'Belarus' UNION
	SELECT 21, 'BE', 'Belgium' UNION
	SELECT 22, 'BZ', 'Belize' UNION
	SELECT 23, 'BJ', 'Benin' UNION
	SELECT 24, 'BM', 'Bermuda' UNION
	SELECT 25, 'BT', 'Bhutan' UNION
	SELECT 26, 'BO', 'Bolivia (Plurinational State of)' UNION
	SELECT 27, 'BQ', 'Bonaire, Sint Eustatius and Saba' UNION
	SELECT 28, 'BA', 'Bosnia and Herzegovina' UNION
	SELECT 29, 'BW', 'Botswana' UNION
	SELECT 30, 'BV', 'Bouvet Island' UNION
	SELECT 31, 'BR', 'Brazil' UNION
	SELECT 32, 'IO', 'British Indian Ocean Territory' UNION
	SELECT 33, 'BN', 'Brunei Darussalam' UNION
	SELECT 34, 'BG', 'Bulgaria' UNION
	SELECT 35, 'BF', 'Burkina Faso' UNION
	SELECT 36, 'BI', 'Burundi' UNION
	SELECT 37, 'CV', 'Cabo Verde' UNION
	SELECT 38, 'KH', 'Cambodia' UNION
	SELECT 39, 'CM', 'Cameroon' UNION
	SELECT 40, 'CA', 'Canada' UNION
	SELECT 41, 'KY', 'Cayman Islands' UNION
	SELECT 42, 'CF', 'Central African Republic' UNION
	SELECT 43, 'TD', 'Chad' UNION
	SELECT 44, 'CL', 'Chile' UNION
	SELECT 45, 'CN', 'China' UNION
	SELECT 46, 'CX', 'Christmas Island' UNION
	SELECT 47, 'CC', 'Cocos (Keeling) Islands' UNION
	SELECT 48, 'CO', 'Colombia' UNION
	SELECT 49, 'KM', 'Comoros' UNION
	SELECT 50, 'CD', 'Congo (the Democratic Republic of the)' UNION
	SELECT 51, 'CG', 'Congo' UNION
	SELECT 52, 'CK', 'Cook Islands' UNION
	SELECT 53, 'CR', 'Costa Rica' UNION
	SELECT 54, 'HR', 'Croatia' UNION
	SELECT 55, 'CU', 'Cuba' UNION
	SELECT 56, 'CW', 'Curaçao' UNION
	SELECT 57, 'CY', 'Cyprus' UNION
	SELECT 58, 'CZ', 'Czechia' UNION
	SELECT 59, 'CI', 'Côte dIvoire' UNION
	SELECT 60, 'DK', 'Denmark' UNION
	SELECT 61, 'DJ', 'Djibouti' UNION
	SELECT 62, 'DM', 'Dominica' UNION
	SELECT 63, 'DO', 'Dominican Republic' UNION
	SELECT 64, 'EC', 'Ecuador' UNION
	SELECT 65, 'EG', 'Egypt' UNION
	SELECT 66, 'SV', 'El Salvador' UNION
	SELECT 67, 'GQ', 'Equatorial Guinea' UNION
	SELECT 68, 'ER', 'Eritrea' UNION
	SELECT 69, 'EE', 'Estonia' UNION
	SELECT 70, 'SZ', 'Eswatini' UNION
	SELECT 71, 'ET', 'Ethiopia' UNION
	SELECT 72, 'FK', 'Falkland Islands [Malvinas]' UNION
	SELECT 73, 'FO', 'Faroe Islands' UNION
	SELECT 74, 'FJ', 'Fiji' UNION
	SELECT 75, 'FI', 'Finland' UNION
	SELECT 76, 'FR', 'France' UNION
	SELECT 77, 'GF', 'French Guiana' UNION
	SELECT 78, 'PF', 'French Polynesia' UNION
	SELECT 79, 'TF', 'French Southern Territories' UNION
	SELECT 80, 'GA', 'Gabon' UNION
	SELECT 81, 'GM', 'Gambia' UNION
	SELECT 82, 'GE', 'Georgia' UNION
	SELECT 83, 'DE', 'Germany' UNION
	SELECT 84, 'GH', 'Ghana' UNION
	SELECT 85, 'GI', 'Gibraltar' UNION
	SELECT 86, 'GR', 'Greece' UNION
	SELECT 87, 'GL', 'Greenland' UNION
	SELECT 88, 'GD', 'Grenada' UNION
	SELECT 89, 'GP', 'Guadeloupe' UNION
	SELECT 90, 'GU', 'Guam' UNION
	SELECT 91, 'GT', 'Guatemala' UNION
	SELECT 92, 'GG', 'Guernsey' UNION
	SELECT 93, 'GN', 'Guinea' UNION
	SELECT 94, 'GW', 'Guinea-Bissau' UNION
	SELECT 95, 'GY', 'Guyana' UNION
	SELECT 96, 'HT', 'Haiti' UNION
	SELECT 97, 'HM', 'Heard Island and McDonald Islands' UNION
	SELECT 98, 'VA', 'Holy See' UNION
	SELECT 99, 'HN', 'Honduras' UNION
	SELECT 100, 'HK', 'Hong Kong' UNION
	SELECT 101, 'HU', 'Hungary' UNION
	SELECT 102, 'IS', 'Iceland' UNION
	SELECT 103, 'IN', 'India' UNION
	SELECT 104, 'ID', 'Indonesia' UNION
	SELECT 105, 'IR', 'Iran (Islamic Republic of)' UNION
	SELECT 106, 'IQ', 'Iraq' UNION
	SELECT 107, 'IE', 'Ireland' UNION
	SELECT 108, 'IM', 'Isle of Man' UNION
	SELECT 109, 'IL', 'Israel' UNION
	SELECT 110, 'IT', 'Italy' UNION
	SELECT 111, 'JM', 'Jamaica' UNION
	SELECT 112, 'JP', 'Japan' UNION
	SELECT 113, 'JE', 'Jersey' UNION
	SELECT 114, 'JO', 'Jordan' UNION
	SELECT 115, 'KZ', 'Kazakhstan' UNION
	SELECT 116, 'KE', 'Kenya' UNION
	SELECT 117, 'KI', 'Kiribati' UNION
	SELECT 118, 'KP', 'Korea (the Democratic People''s Republic of)' UNION
	SELECT 119, 'KR', 'Korea (the Republic of)' UNION
	SELECT 120, 'KW', 'Kuwait' UNION
	SELECT 121, 'KG', 'Kyrgyzstan' UNION
	SELECT 122, 'LA', 'Lao People''s Democratic Republic' UNION
	SELECT 123, 'LV', 'Latvia' UNION
	SELECT 124, 'LB', 'Lebanon' UNION
	SELECT 125, 'LS', 'Lesotho' UNION
	SELECT 126, 'LR', 'Liberia' UNION
	SELECT 127, 'LY', 'Libya' UNION
	SELECT 128, 'LI', 'Liechtenstein' UNION
	SELECT 129, 'LT', 'Lithuania' UNION
	SELECT 130, 'LU', 'Luxembourg' UNION
	SELECT 131, 'MO', 'Macao' UNION
	SELECT 132, 'MG', 'Madagascar' UNION
	SELECT 133, 'MW', 'Malawi' UNION
	SELECT 134, 'MY', 'Malaysia' UNION
	SELECT 135, 'MV', 'Maldives' UNION
	SELECT 136, 'ML', 'Mali' UNION
	SELECT 137, 'MT', 'Malta' UNION
	SELECT 138, 'MH', 'Marshall Islands' UNION
	SELECT 139, 'MQ', 'Martinique' UNION
	SELECT 140, 'MR', 'Mauritania' UNION
	SELECT 141, 'MU', 'Mauritius' UNION
	SELECT 142, 'YT', 'Mayotte' UNION
	SELECT 143, 'MX', 'Mexico' UNION
	SELECT 144, 'FM', 'Micronesia (Federated States of)' UNION
	SELECT 145, 'MD', 'Moldova (the Republic of)' UNION
	SELECT 146, 'MC', 'Monaco' UNION
	SELECT 147, 'MN', 'Mongolia' UNION
	SELECT 148, 'ME', 'Montenegro' UNION
	SELECT 149, 'MS', 'Montserrat' UNION
	SELECT 150, 'MA', 'Morocco' UNION
	SELECT 151, 'MZ', 'Mozambique' UNION
	SELECT 152, 'MM', 'Myanmar' UNION
	SELECT 153, 'NA', 'Namibia' UNION
	SELECT 154, 'NR', 'Nauru' UNION
	SELECT 155, 'NP', 'Nepal' UNION
	SELECT 156, 'NL', 'Netherlands' UNION
	SELECT 157, 'NC', 'New Caledonia' UNION
	SELECT 158, 'NZ', 'New Zealand' UNION
	SELECT 159, 'NI', 'Nicaragua' UNION
	SELECT 160, 'NE', 'Niger' UNION
	SELECT 161, 'NG', 'Nigeria' UNION
	SELECT 162, 'NU', 'Niue' UNION
	SELECT 163, 'NF', 'Norfolk Island' UNION
	SELECT 164, 'MP', 'Northern Mariana Islands' UNION
	SELECT 165, 'NO', 'Norway' UNION
	SELECT 166, 'OM', 'Oman' UNION
	SELECT 167, 'PK', 'Pakistan' UNION
	SELECT 168, 'PW', 'Palau' UNION
	SELECT 169, 'PS', 'Palestine, State of' UNION
	SELECT 170, 'PA', 'Panama' UNION
	SELECT 171, 'PG', 'Papua New Guinea' UNION
	SELECT 172, 'PY', 'Paraguay' UNION
	SELECT 173, 'PE', 'Peru' UNION
	SELECT 174, 'PH', 'Philippines' UNION
	SELECT 175, 'PN', 'Pitcairn' UNION
	SELECT 176, 'PL', 'Poland' UNION
	SELECT 177, 'PT', 'Portugal' UNION
	SELECT 178, 'PR', 'Puerto Rico' UNION
	SELECT 179, 'QA', 'Qatar' UNION
	SELECT 180, 'MK', 'Republic of North Macedonia' UNION
	SELECT 181, 'RO', 'Romania' UNION
	SELECT 182, 'RU', 'Russian Federation' UNION
	SELECT 183, 'RW', 'Rwanda' UNION
	SELECT 184, 'RE', 'Réunion' UNION
	SELECT 185, 'BL', 'Saint Barthelemy' UNION
	SELECT 186, 'SH', 'Saint Helena, Ascension and Tristan da Cunha' UNION
	SELECT 187, 'KN', 'Saint Kitts and Nevis' UNION
	SELECT 188, 'LC', 'Saint Lucia' UNION
	SELECT 189, 'MF', 'Saint Martin (French part)' UNION
	SELECT 190, 'PM', 'Saint Pierre and Miquelon' UNION
	SELECT 191, 'VC', 'Saint Vincent and the Grenadines' UNION
	SELECT 192, 'WS', 'Samoa' UNION
	SELECT 193, 'SM', 'San Marino' UNION
	SELECT 194, 'ST', 'Sao Tome and Principe' UNION
	SELECT 195, 'SA', 'Saudi Arabia' UNION
	SELECT 196, 'SN', 'Senegal' UNION
	SELECT 197, 'RS', 'Serbia' UNION
	SELECT 198, 'SC', 'Seychelles' UNION
	SELECT 199, 'SL', 'Sierra Leone' UNION
	SELECT 200, 'SG', 'Singapore' UNION
	SELECT 201, 'SX', 'Sint Maarten (Dutch part)' UNION
	SELECT 202, 'SK', 'Slovakia' UNION
	SELECT 203, 'SI', 'Slovenia' UNION
	SELECT 204, 'SB', 'Solomon Islands' UNION
	SELECT 205, 'SO', 'Somalia' UNION
	SELECT 206, 'ZA', 'South Africa' UNION
	SELECT 207, 'GS', 'South Georgia and the South Sandwich Islands' UNION
	SELECT 208, 'SS', 'South Sudan' UNION
	SELECT 209, 'ES', 'Spain' UNION
	SELECT 210, 'LK', 'Sri Lanka' UNION
	SELECT 211, 'SD', 'Sudan' UNION
	SELECT 212, 'SR', 'Suriname' UNION
	SELECT 213, 'SJ', 'Svalbard and Jan Mayen' UNION
	SELECT 214, 'SE', 'Sweden' UNION
	SELECT 215, 'CH', 'Switzerland' UNION
	SELECT 216, 'SY', 'Syrian Arab Republic' UNION
	SELECT 217, 'TW', 'Taiwan (Province of China)' UNION
	SELECT 218, 'TJ', 'Tajikistan' UNION
	SELECT 219, 'TZ', 'Tanzania, United Republic of' UNION
	SELECT 220, 'TH', 'Thailand' UNION
	SELECT 221, 'TL', 'Timor-Leste' UNION
	SELECT 222, 'TG', 'Togo' UNION
	SELECT 223, 'TK', 'Tokelau' UNION
	SELECT 224, 'TO', 'Tonga' UNION
	SELECT 225, 'TT', 'Trinidad and Tobago' UNION
	SELECT 226, 'TN', 'Tunisia' UNION
	SELECT 227, 'TR', 'Turkey' UNION
	SELECT 228, 'TM', 'Turkmenistan' UNION
	SELECT 229, 'TC', 'Turks and Caicos Islands' UNION
	SELECT 230, 'TV', 'Tuvalu' UNION
	SELECT 231, 'UG', 'Uganda' UNION
	SELECT 232, 'UA', 'Ukraine' UNION
	SELECT 233, 'AE', 'United Arab Emirates' UNION
	SELECT 234, 'GB', 'United Kingdom of Great Britain ? Northern Ireland' UNION
	SELECT 235, 'UM', 'United States Minor Outlying Islands' UNION
	SELECT 236, 'US', 'United States of America' UNION
	SELECT 237, 'UY', 'Uruguay' UNION
	SELECT 238, 'UZ', 'Uzbekistan' UNION
	SELECT 239, 'VU', 'Vanuatu' UNION
	SELECT 240, 'VE', 'Venezuela (Bolivarian Republic of)' UNION
	SELECT 241, 'VN', 'Viet Nam' UNION
	SELECT 242, 'VG', 'Virgin Islands (British)' UNION
	SELECT 243, 'VI', 'Virgin Islands (U.S.)' UNION
	SELECT 244, 'WF', 'Wallis and Futuna' UNION
	SELECT 245, 'EH', 'Western Sahara' UNION
	SELECT 246, 'YE', 'Yemen' UNION
	SELECT 247, 'ZM', 'Zambia' UNION
	SELECT 248, 'ZW', 'Zimbabwe' 
	


	SET IDENTITY_INSERT dbo.Country ON;

	MERGE dbo.Country AS t
	USING @tblCountry AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[CountryName] = s.[CountryName],
			t.[ISO] = s.[ISO]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[CountryName], [ISO]) 
		VALUES (s.[ID], s.[CountryName], s.[ISO])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.Country OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_Currency_Populate'))
   DROP PROC dbo.p_Currency_Populate
GO

CREATE PROCEDURE dbo.p_Currency_Populate 
	
AS
BEGIN

	DECLARE @tblCurrency AS TABLE (
		[ID] [bigint] NOT NULL,
		[ISO] [nvarchar](5) NOT NULL,
		[CurrencyName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblCurrency
	SELECT 1, 'AFN', 'Afghani' UNION 
	SELECT 2, 'EUR', 'Euro' UNION 
	SELECT 3, 'ALL', 'Lek' UNION 
	SELECT 4, 'DZD', 'Algerian Dinar' UNION 
	SELECT 5, 'USD', 'US Dollar' UNION 
	SELECT 6, 'EUR', 'Euro' UNION 
	SELECT 7, 'AOA', 'Kwanza' UNION 
	SELECT 8, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 9, 'ARS', 'Argentine Peso' UNION 
	SELECT 10, 'AMD', 'Armenian Dram' UNION 
	SELECT 11, 'AWG', 'Aruban Florin' UNION 
	SELECT 12, 'AUD', 'Australian Dollar' UNION 
	SELECT 13, 'EUR', 'Euro' UNION 
	SELECT 14, 'AZN', 'Azerbaijanian Manat' UNION 
	SELECT 15, 'BSD', 'Bahamian Dollar' UNION 
	SELECT 16, 'BHD', 'Bahraini Dinar' UNION 
	SELECT 17, 'BDT', 'Taka' UNION 
	SELECT 18, 'BBD', 'Barbados Dollar' UNION 
	SELECT 19, 'BYR', 'Belarussian Ruble' UNION 
	SELECT 20, 'EUR', 'Euro' UNION 
	SELECT 21, 'BZD', 'Belize Dollar' UNION 
	SELECT 22, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 23, 'BMD', 'Bermudian Dollar' UNION 
	SELECT 24, 'BTN', 'Ngultrum' UNION 
	SELECT 25, 'INR', 'Indian Rupee' UNION 
	SELECT 26, 'BOB', 'Boliviano' UNION 
	SELECT 27, 'BOV', 'Mvdol' UNION 
	SELECT 28, 'USD', 'US Dollar' UNION 
	SELECT 29, 'BAM', 'Convertible Mark' UNION 
	SELECT 30, 'BWP', 'Pula' UNION 
	SELECT 31, 'NOK', 'Norwegian Krone' UNION 
	SELECT 32, 'BRL', 'Brazilian Real' UNION 
	SELECT 33, 'USD', 'US Dollar' UNION 
	SELECT 34, 'BND', 'Brunei Dollar' UNION 
	SELECT 35, 'BGN', 'Bulgarian Lev' UNION 
	SELECT 36, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 37, 'BIF', 'Burundi Franc' UNION 
	SELECT 38, 'KHR', 'Riel' UNION 
	SELECT 39, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 40, 'CAD', 'Canadian Dollar' UNION 
	SELECT 41, 'CVE', 'Cabo Verde Escudo' UNION 
	SELECT 42, 'KYD', 'Cayman Islands Dollar' UNION 
	SELECT 43, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 44, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 45, 'CLF', 'Unidad de Fomento' UNION 
	SELECT 46, 'CLP', 'Chilean Peso' UNION 
	SELECT 47, 'CNY', 'Yuan Renminbi' UNION 
	SELECT 48, 'AUD', 'Australian Dollar' UNION 
	SELECT 49, 'AUD', 'Australian Dollar' UNION 
	SELECT 50, 'COP', 'Colombian Peso' UNION 
	SELECT 51, 'COU', 'Unidad de Valor Real' UNION 
	SELECT 52, 'KMF', 'Comoro Franc' UNION 
	SELECT 53, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 54, 'CDF', 'Congolese Franc' UNION 
	SELECT 55, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 56, 'CRC', 'Costa Rican Colon' UNION 
	SELECT 57, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 58, 'HRK', 'Croatian Kuna' UNION 
	SELECT 59, 'CUC', 'Peso Convertible' UNION 
	SELECT 60, 'CUP', 'Cuban Peso' UNION 
	SELECT 61, 'ANG', 'Netherlands Antillean Guilder' UNION 
	SELECT 62, 'EUR', 'Euro' UNION 
	SELECT 63, 'CZK', 'Czech Koruna' UNION 
	SELECT 64, 'DKK', 'Danish Krone' UNION 
	SELECT 65, 'DJF', 'Djibouti Franc' UNION 
	SELECT 66, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 67, 'DOP', 'Dominican Peso' UNION 
	SELECT 68, 'USD', 'US Dollar' UNION 
	SELECT 69, 'EGP', 'Egyptian Pound' UNION 
	SELECT 70, 'SVC', 'El Salvador Colon' UNION 
	SELECT 71, 'USD', 'US Dollar' UNION 
	SELECT 72, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 73, 'ERN', 'Nakfa' UNION 
	SELECT 74, 'EUR', 'Euro' UNION 
	SELECT 75, 'ETB', 'Ethiopian Birr' UNION 
	SELECT 76, 'EUR', 'Euro' UNION 
	SELECT 77, 'FKP', 'Falkland Islands Pound' UNION 
	SELECT 78, 'DKK', 'Danish Krone' UNION 
	SELECT 79, 'FJD', 'Fiji Dollar' UNION 
	SELECT 80, 'EUR', 'Euro' UNION 
	SELECT 81, 'EUR', 'Euro' UNION 
	SELECT 82, 'EUR', 'Euro' UNION 
	SELECT 83, 'XPF', 'CFP Franc' UNION 
	SELECT 84, 'EUR', 'Euro' UNION 
	SELECT 85, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 86, 'GMD', 'Dalasi' UNION 
	SELECT 87, 'GEL', 'Lari' UNION 
	SELECT 88, 'EUR', 'Euro' UNION 
	SELECT 89, 'GHS', 'Ghana Cedi' UNION 
	SELECT 90, 'GIP', 'Gibraltar Pound' UNION 
	SELECT 91, 'EUR', 'Euro' UNION 
	SELECT 92, 'DKK', 'Danish Krone' UNION 
	SELECT 93, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 94, 'EUR', 'Euro' UNION 
	SELECT 95, 'USD', 'US Dollar' UNION 
	SELECT 96, 'GTQ', 'Quetzal' UNION 
	SELECT 97, 'GBP', 'Pound Sterling' UNION 
	SELECT 98, 'GNF', 'Guinea Franc' UNION 
	SELECT 99, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 100, 'GYD', 'Guyana Dollar' UNION 
	SELECT 101, 'HTG', 'Gourde' UNION 
	SELECT 102, 'USD', 'US Dollar' UNION 
	SELECT 103, 'AUD', 'Australian Dollar' UNION 
	SELECT 104, 'EUR', 'Euro' UNION 
	SELECT 105, 'HNL', 'Lempira' UNION 
	SELECT 106, 'HKD', 'Hong Kong Dollar' UNION 
	SELECT 107, 'HUF', 'Forint' UNION 
	SELECT 108, 'ISK', 'Iceland Krona' UNION 
	SELECT 109, 'INR', 'Indian Rupee' UNION 
	SELECT 110, 'IDR', 'Rupiah' UNION 
	SELECT 111, 'XDR', 'SDR (Special Drawing Right)' UNION 
	SELECT 112, 'IRR', 'Iranian Rial' UNION 
	SELECT 113, 'IQD', 'Iraqi Dinar' UNION 
	SELECT 114, 'EUR', 'Euro' UNION 
	SELECT 115, 'GBP', 'Pound Sterling' UNION 
	SELECT 116, 'ILS', 'New Israeli Sheqel' UNION 
	SELECT 117, 'EUR', 'Euro' UNION 
	SELECT 118, 'JMD', 'Jamaican Dollar' UNION 
	SELECT 119, 'JPY', 'Yen' UNION 
	SELECT 120, 'GBP', 'Pound Sterling' UNION 
	SELECT 121, 'JOD', 'Jordanian Dinar' UNION 
	SELECT 122, 'KZT', 'Tenge' UNION 
	SELECT 123, 'KES', 'Kenyan Shilling' UNION 
	SELECT 124, 'AUD', 'Australian Dollar' UNION 
	SELECT 125, 'KPW', 'North Korean Won' UNION 
	SELECT 126, 'KRW', 'Won' UNION 
	SELECT 127, 'KWD', 'Kuwaiti Dinar' UNION 
	SELECT 128, 'KGS', 'Som' UNION 
	SELECT 129, 'LAK', 'Kip' UNION 
	SELECT 130, 'EUR', 'Euro' UNION 
	SELECT 131, 'LBP', 'Lebanese Pound' UNION 
	SELECT 132, 'LSL', 'Loti' UNION 
	SELECT 133, 'ZAR', 'Rand' UNION 
	SELECT 134, 'LRD', 'Liberian Dollar' UNION 
	SELECT 135, 'LYD', 'Libyan Dinar' UNION 
	SELECT 136, 'CHF', 'Swiss Franc' UNION 
	SELECT 137, 'EUR', 'Euro' UNION 
	SELECT 138, 'EUR', 'Euro' UNION 
	SELECT 139, 'MOP', 'Pataca' UNION 
	SELECT 140, 'MKD', 'Denar' UNION 
	SELECT 141, 'MGA', 'Malagasy Ariary' UNION 
	SELECT 142, 'MWK', 'Kwacha' UNION 
	SELECT 143, 'MYR', 'Malaysian Ringgit' UNION 
	SELECT 144, 'MVR', 'Rufiyaa' UNION 
	SELECT 145, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 146, 'EUR', 'Euro' UNION 
	SELECT 147, 'USD', 'US Dollar' UNION 
	SELECT 148, 'EUR', 'Euro' UNION 
	SELECT 149, 'MRO', 'Ouguiya' UNION 
	SELECT 150, 'MUR', 'Mauritius Rupee' UNION 
	SELECT 151, 'EUR', 'Euro' UNION 
	SELECT 152, 'XUA', 'ADB Unit of Account' UNION 
	SELECT 153, 'MXN', 'Mexican Peso' UNION 
	SELECT 154, 'MXV', 'Mexican Unidad de Inversion (UDI)' UNION 
	SELECT 155, 'USD', 'US Dollar' UNION 
	SELECT 156, 'MDL', 'Moldovan Leu' UNION 
	SELECT 157, 'EUR', 'Euro' UNION 
	SELECT 158, 'MNT', 'Tugrik' UNION 
	SELECT 159, 'EUR', 'Euro' UNION 
	SELECT 160, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 161, 'MAD', 'Moroccan Dirham' UNION 
	SELECT 162, 'MZN', 'Mozambique Metical' UNION 
	SELECT 163, 'MMK', 'Kyat' UNION 
	SELECT 164, 'NAD', 'Namibia Dollar' UNION 
	SELECT 165, 'ZAR', 'Rand' UNION 
	SELECT 166, 'AUD', 'Australian Dollar' UNION 
	SELECT 167, 'NPR', 'Nepalese Rupee' UNION 
	SELECT 168, 'EUR', 'Euro' UNION 
	SELECT 169, 'XPF', 'CFP Franc' UNION 
	SELECT 170, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 171, 'NIO', 'Cordoba Oro' UNION 
	SELECT 172, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 173, 'NGN', 'Naira' UNION 
	SELECT 174, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 175, 'AUD', 'Australian Dollar' UNION 
	SELECT 176, 'USD', 'US Dollar' UNION 
	SELECT 177, 'NOK', 'Norwegian Krone' UNION 
	SELECT 178, 'OMR', 'Rial Omani' UNION 
	SELECT 179, 'PKR', 'Pakistan Rupee' UNION 
	SELECT 180, 'USD', 'US Dollar' UNION 
	SELECT 181, 'PAB', 'Balboa' UNION 
	SELECT 182, 'USD', 'US Dollar' UNION 
	SELECT 183, 'PGK', 'Kina' UNION 
	SELECT 184, 'PYG', 'Guarani' UNION 
	SELECT 185, 'PEN', 'Nuevo Sol' UNION 
	SELECT 186, 'PHP', 'Philippine Peso' UNION 
	SELECT 187, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 188, 'PLN', 'Zloty' UNION 
	SELECT 189, 'EUR', 'Euro' UNION 
	SELECT 190, 'USD', 'US Dollar' UNION 
	SELECT 191, 'QAR', 'Qatari Rial' UNION 
	SELECT 192, 'EUR', 'Euro' UNION 
	SELECT 193, 'RON', 'New Romanian Leu' UNION 
	SELECT 194, 'RUB', 'Russian Ruble' UNION 
	SELECT 195, 'RWF', 'Rwanda Franc' UNION 
	SELECT 196, 'EUR', 'Euro' UNION 
	SELECT 197, 'SHP', 'Saint Helena Pound' UNION 
	SELECT 198, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 199, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 200, 'EUR', 'Euro' UNION 
	SELECT 201, 'EUR', 'Euro' UNION 
	SELECT 202, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 203, 'WST', 'Tala' UNION 
	SELECT 204, 'EUR', 'Euro' UNION 
	SELECT 205, 'STD', 'Dobra' UNION 
	SELECT 206, 'SAR', 'Saudi Riyal' UNION 
	SELECT 207, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 208, 'RSD', 'Serbian Dinar' UNION 
	SELECT 209, 'SCR', 'Seychelles Rupee' UNION 
	SELECT 210, 'SLL', 'Leone' UNION 
	SELECT 211, 'SGD', 'Singapore Dollar' UNION 
	SELECT 212, 'ANG', 'Netherlands Antillean Guilder' UNION 
	SELECT 213, 'XSU', 'Sucre' UNION 
	SELECT 214, 'EUR', 'Euro' UNION 
	SELECT 215, 'EUR', 'Euro' UNION 
	SELECT 216, 'SBD', 'Solomon Islands Dollar' UNION 
	SELECT 217, 'SOS', 'Somali Shilling' UNION 
	SELECT 218, 'ZAR', 'Rand' UNION 
	SELECT 219, 'SSP', 'South Sudanese Pound' UNION 
	SELECT 220, 'EUR', 'Euro' UNION 
	SELECT 221, 'LKR', 'Sri Lanka Rupee' UNION 
	SELECT 222, 'SDG', 'Sudanese Pound' UNION 
	SELECT 223, 'SRD', 'Surinam Dollar' UNION 
	SELECT 224, 'NOK', 'Norwegian Krone' UNION 
	SELECT 225, 'SZL', 'Lilangeni' UNION 
	SELECT 226, 'SEK', 'Swedish Krona' UNION 
	SELECT 227, 'CHE', 'WIR Euro' UNION 
	SELECT 228, 'CHF', 'Swiss Franc' UNION 
	SELECT 229, 'CHW', 'WIR Franc' UNION 
	SELECT 230, 'SYP', 'Syrian Pound' UNION 
	SELECT 231, 'TWD', 'New Taiwan Dollar' UNION 
	SELECT 232, 'TJS', 'Somoni' UNION 
	SELECT 233, 'TZS', 'Tanzanian Shilling' UNION 
	SELECT 234, 'THB', 'Baht' UNION 
	SELECT 235, 'USD', 'US Dollar' UNION 
	SELECT 236, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 237, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 238, 'TOP', 'Paanga' UNION 
	SELECT 239, 'TTD', 'Trinidad and Tobago Dollar' UNION 
	SELECT 240, 'TND', 'Tunisian Dinar' UNION 
	SELECT 241, 'TRY', 'Turkish Lira' UNION 
	SELECT 242, 'TMT', 'Turkmenistan New Manat' UNION 
	SELECT 243, 'USD', 'US Dollar' UNION 
	SELECT 244, 'AUD', 'Australian Dollar' UNION 
	SELECT 245, 'UGX', 'Uganda Shilling' UNION 
	SELECT 246, 'UAH', 'Hryvnia' UNION 
	SELECT 247, 'AED', 'UAE Dirham' UNION 
	SELECT 248, 'GBP', 'Pound Sterling' UNION 
	SELECT 249, 'USD', 'US Dollar' UNION 
	SELECT 250, 'USN', 'US Dollar (Next day)' UNION 
	SELECT 251, 'USD', 'US Dollar' UNION 
	SELECT 252, 'UYI', 'Uruguay Peso en Unidades Indexadas (URUIURUI)' UNION 
	SELECT 253, 'UYU', 'Peso Uruguayo' UNION 
	SELECT 254, 'UZS', 'Uzbekistan Sum' UNION 
	SELECT 255, 'VUV', 'Vatu' UNION 
	SELECT 256, 'VEF', 'Bolivar' UNION 
	SELECT 257, 'VND', 'Dong' UNION 
	SELECT 258, 'USD', 'US Dollar' UNION 
	SELECT 259, 'USD', 'US Dollar' UNION 
	SELECT 260, 'XPF', 'CFP Franc' UNION 
	SELECT 261, 'MAD', 'Moroccan Dirham' UNION 
	SELECT 262, 'YER', 'Yemeni Rial' UNION 
	SELECT 263, 'ZMW', 'Zambian Kwacha' UNION 
	SELECT 264, 'ZWL', 'Zimbabwe Dollar'  

	


	SET IDENTITY_INSERT dbo.Currency ON;

	MERGE dbo.Currency AS t
	USING @tblCurrency AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[CurrencyName] = s.[CurrencyName],
			t.[ISO] = s.[ISO]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[CurrencyName], [ISO]) 
		VALUES (s.[ID], s.[CurrencyName], s.[ISO])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.Currency OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_OrderStatus_Populate'))
   DROP PROC dbo.p_OrderStatus_Populate
GO

CREATE PROCEDURE dbo.p_OrderStatus_Populate 
	
AS
BEGIN

	DECLARE @tblOrderStatus AS TABLE (
		[ID] [bigint] NOT NULL,
		[OrderStatusName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblOrderStatus
	SELECT 1,	'New' UNION
	SELECT 2,	'ProductionReady' UNION
	SELECT 3,	'SentToProduction' UNION
	SELECT 4,	'InProduction' UNION
	SELECT 5,	'Produced' UNION
	SELECT 6,	'SentToOffice' UNION
	SELECT 7,	'PrepareDelivery' UNION
	SELECT 8,	'AwaitSending' UNION
	SELECT 9,	'Sent' UNION
	SELECT 10,	'Received' UNION
	SELECT 11,	'Cancelled'

	SET IDENTITY_INSERT dbo.OrderStatus ON;

	MERGE dbo.OrderStatus AS t
	USING @tblOrderStatus AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[OrderStatusName] = s.[OrderStatusName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[OrderStatusName]) VALUES (s.[ID], s.[OrderStatusName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.OrderStatus OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_PaymentMethod_Populate'))
   DROP PROC dbo.p_PaymentMethod_Populate
GO

CREATE PROCEDURE dbo.p_PaymentMethod_Populate 
	
AS
BEGIN

	DECLARE @tblPaymentMethod AS TABLE (
		[ID] [bigint] NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](1000) NULL,
		[IsDeleted] [bit] NOT NULL
	)

	INSERT INTO @tblPaymentMethod
	SELECT 1, 'Visa', NULL, 0	UNION 
	SELECT 2, 'Mastercard', NULL, 0	UNION
	SELECT 3, 'PayPal', NULL, 0	UNION
	SELECT 4, 'Wire transfer', NULL, 0	UNION
	SELECT 5, 'Skrill', NULL, 0				


	SET IDENTITY_INSERT dbo.PaymentMethod ON;

	MERGE dbo.PaymentMethod AS t
	USING @tblPaymentMethod AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[Name] = s.[Name],
			t.[Description] = s.[Description],
			t.[IsDeleted] = s.[IsDeleted]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[Name],[Description],[IsDeleted]) VALUES (s.[ID],s.[Name],s.[Description],s.[IsDeleted])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.PaymentMethod OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_Region_Populate'))
   DROP PROC dbo.p_Region_Populate
GO

CREATE PROCEDURE dbo.p_Region_Populate 
	
AS
BEGIN

	DECLARE @tblRegion AS TABLE (
		[ID] [bigint] NOT NULL,
		[RegionName] [nvarchar](50) NOT NULL,
		[CountryID] [bigint] NOT NULL
	)

	INSERT INTO @tblRegion
	-- UKRAINE
	SELECT 1, 'Kharkivska obl.', 232	UNION 
	SELECT 2, 'Kyivska obl.', 232		UNION
	SELECT 3, 'Dniprovska obl.', 232 UNION
	SELECT 4, 'Rivnenska obl.', 232 UNION
	SELECT 5, 'Zhytomyrska obl.', 232 UNION
	SELECT 6, 'All Ukraine', 232 UNION
	-- POLAND
	SELECT 7, 'All Poland', 176 UNION
	SELECT 8, 'Pomorske voevodship', 176 UNION
	SELECT 9, 'Mazowieckie voevodship', 176

	SET IDENTITY_INSERT dbo.Region ON;

	MERGE dbo.Region AS t
	USING @tblRegion AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[RegionName] = s.[RegionName],
			t.[CountryID] = s.[CountryID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[RegionName], [CountryID]) VALUES (s.[ID], s.[RegionName], s.[CountryID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.Region OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_CleanUp'))
   DROP PROC dbo.p_TestData_CleanUp
GO

/*
Usage:
EXEC dbo.p_TestData_CleanUp
*/
CREATE PROCEDURE dbo.p_TestData_CleanUp 
	
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @tblParams AS TABLE (
		[Order] INT,
		[Table] NVARCHAR(100),
		[File]  NVARCHAR(100)
	)

	DECLARE @file AS NVARCHAR(100) 
	DECLARE @table AS NVARCHAR(100) 

	INSERT INTO @tblParams	
	

	SELECT -1, 'OrderStatusFlow', 'OrderStatusFlow.csv'	UNION
	SELECT -1, 'OrderPaymentDetails', 'OrderPaymentDetails.csv'				UNION
	SELECT -1, 'OrderItem', 'OrderItem.csv'				UNION
	SELECT -1, 'OrderTracking', 'OrderTracking.csv'		UNION
	SELECT 0, 'Order', 'Order.csv'						UNION
	SELECT 1, 'DeliveryServiceCity', 'DeliveryServiceCity.csv'	UNION
	SELECT 2, 'DeliveryService', 'DeliveryService.csv'	UNION
	SELECT 3, 'UserContact', 'UserContact.csv'			UNION
	SELECT 4, 'UserAddress', 'UserAddress.csv'			UNION
	SELECT 5, 'PrintingHouseAddress', 'PrintingHouseAddresss.csv'		UNION
	SELECT 6, 'PrintingHouseContact', 'PrintingHouseContact.csv'		UNION
	SELECT 7, 'Address', 'Address.csv'					UNION
	SELECT 8, 'Contact', 'Contact.csv'					UNION
	SELECT 9, 'Size', 'Size.csv'						UNION
	SELECT 10, 'ImageCategory', 'ImageCategory.csv'		UNION
	SELECT 11, 'Category', 'Category.csv'				UNION	
	SELECT 12, 'FrameType', 'FrameType.csv'				UNION	
	SELECT 13, 'Mat', 'Mat.csv'							UNION
	SELECT 14, 'MaterialType', 'MaterialType.csv'		UNION
	SELECT 15, 'MountingType', 'MountingType.csv'		UNION
	SELECT 16, 'PrintingHouse', 'PrintingHouse.csv'		UNION
	SELECT 17, 'ImageRelated', 'ImageRelated.csv'		UNION
	SELECT 17, 'ImageThumbnail', 'ImageThumbnail.csv'	UNION
	SELECT 18, 'Image', 'Image.csv'						UNION
	SELECT 19, 'UserConfirmation', 'UserConfirmation.csv'	UNION
	SELECT 99, 'User', 'User.csv'						

	DECLARE paramsCursor CURSOR FOR
	SELECT [File], [Table] FROM @tblParams ORDER BY [Order]

	DECLARE @sql AS NVARCHAR(MAX)
	
	OPEN paramsCursor 

	BEGIN TRY

		BEGIN TRANSACTION

		FETCH NEXT FROM paramsCursor INTO @file, @table

		WHILE @@FETCH_STATUS = 0
		BEGIN

			PRINT('======= ' + @table + ' =======')

			SET @sql = 'DELETE FROM dbo.[' + @table + ']'
			PRINT(@sql)

			EXEC(@sql);

			FETCH NEXT FROM paramsCursor INTO @file, @table

		END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT   
        ERROR_NUMBER() AS ErrorNumber  
       ,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH

	CLOSE paramsCursor
	DEALLOCATE paramsCursor
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_Populate'))
   DROP PROC dbo.p_TestData_Populate
GO

/*
Usage:
EXEC p_TestData_Populate 'D:\Projects\PhotoPrint\Testing\TestData\'
*/
CREATE PROCEDURE p_TestData_Populate
	@RootFolder NVARCHAR(100)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @tblParams AS TABLE (
		[Order] INT,
		[Table] NVARCHAR(100),
		[File]  NVARCHAR(100),
		[HasIdentity] BIT
	)

	DECLARE @file AS NVARCHAR(100) 
	DECLARE @table AS NVARCHAR(100) 
	DECLARE @hasIdentity AS BIT 

	INSERT INTO @tblParams
	
	SELECT 1, 'DeliveryService', 'DeliveryService.csv', 1			UNION
	SELECT 2, 'DeliveryServiceCity', 'DeliveryServiceCity.csv', 0	UNION
	SELECT 3, 'User', 'User.csv', 1									UNION
	SELECT 4, 'Address', 'Address.csv', 1							UNION
	SELECT 5, 'UserAddress', 'UserAddress.csv', 0					UNION
	SELECT 6, 'Contact', 'Contact.csv', 1							UNION
	SELECT 7, 'UserContact', 'UserContact.csv', 0					UNION
	SELECT 8, 'Size', 'Size.csv', 1									UNION
	SELECT 9, 'Category', 'Category.csv', 1							UNION
	SELECT 10, 'FrameType', 'FrameType.csv', 1						UNION
	SELECT 11, 'Mat', 'Mat.csv', 1									UNION
	SELECT 12, 'MaterialType', 'MaterialType.csv', 1				UNION
	SELECT 12, 'MountingType', 'MountingType.csv', 1				UNION
	SELECT 13, 'PrintingHouse', 'PrintingHouse.csv', 1				UNION
	SELECT 14, 'Image', 'Image.csv', 1								UNION
	SELECT 15, 'ImageCategory', 'ImageCategory.csv', 0				UNION
	SELECT 16, 'ImageRelated', 'ImageRelated.csv', 0				UNION
	SELECT 17, 'ImageThumbnail', 'ImageThumbnail.csv', 1			UNION
	SELECT 18, 'PrintingHouseAddress', 'PrintingHouseAddress.csv', 0	UNION
	SELECT 19, 'PrintingHouseContact', 'PrintingHouseContact.csv', 0	UNION
	SELECT 20, 'Order', 'Order.csv', 1								UNION
	SELECT 21, 'OrderItem', 'OrderItem.csv', 1						UNION
	SELECT 22, 'OrderPaymentDetails', 'OrderPaymentDetails.csv', 1	UNION
	SELECT 23, 'OrderStatusFlow', 'OrderStatusFlow.csv', 0			UNION
	SELECT 24, 'OrderTracking', 'OrderTracking.csv', 1				UNION
	SELECT 25, 'UserConfirmation', 'UserConfirmation.csv', 1


	DECLARE paramsCursor CURSOR FOR
	SELECT [File], [Table], [HasIdentity] FROM @tblParams ORDER BY [Order]
	
	DECLARE @Path AS NVARCHAR(MAX)
	DECLARE @sql AS NVARCHAR(MAX)
	
	OPEN paramsCursor 

	BEGIN TRY

		BEGIN TRANSACTION

		FETCH NEXT FROM paramsCursor INTO @file, @table, @hasIdentity

		WHILE @@FETCH_STATUS = 0
		BEGIN

			PRINT('======= ' + @file + ' -> ' + @table + ' =======')

			SELECT @Path = CONCAT(@RootFolder, @file)
			IF(@hasIdentity = 1) BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @table + '] ON;'

				PRINT(@sql)
				EXEC(@sql)
			END


			SET @sql = 'BULK INSERT dbo.[' + @table + ']
			FROM ''' + @Path + '''
			WITH (
			KEEPIDENTITY,
			FIRSTROW = 2,
			FIELDTERMINATOR = '','',
			ROWTERMINATOR=''0x0d0a'',
			BATCHSIZE=2500000);'

			PRINT(@sql)
			EXEC(@sql)

			IF(@hasIdentity = 1) 
			BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @table + '] OFF;'

				PRINT(@sql)
				EXEC(@sql)
			END

			FETCH NEXT FROM paramsCursor INTO @file, @table, @hasIdentity

		END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT   
        ERROR_NUMBER() AS ErrorNumber  
       ,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH

	CLOSE paramsCursor
	DEALLOCATE paramsCursor
    
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_Unit_Populate'))
   DROP PROC dbo.p_Unit_Populate
GO

CREATE PROCEDURE dbo.p_Unit_Populate 
	
AS
BEGIN

	DECLARE @tblUnit AS TABLE (
		[ID] [bigint] NOT NULL,
		[UnitName] [nvarchar](50) NOT NULL,
		[UnitAbbr] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](100) 
	)

	INSERT INTO @tblUnit
	SELECT 1, 'Square meter', 'm2', NULL	UNION 
	SELECT 2, 'Meter', 'm', NULL			UNION
	SELECT 3, 'Centimeter', 'cm', NULL		UNION
	SELECT 4, 'Square centimeter', 'cm2', NULL		UNION
	SELECT 5, 'Item', 'item', NULL			


	SET IDENTITY_INSERT dbo.Unit ON;

	MERGE dbo.Unit AS t
	USING @tblUnit AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[UnitName] = s.[UnitName],
			t.[UnitAbbr] = s.[UnitAbbr],
			t.[Description] = s.[Description]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[UnitName],[UnitAbbr],[Description]) VALUES (s.[ID], s.[UnitName],s.[UnitAbbr],s.[Description])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.Unit OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_UserStatus_Populate'))
   DROP PROC dbo.p_UserStatus_Populate
GO

CREATE PROCEDURE dbo.p_UserStatus_Populate 
	
AS
BEGIN

	DECLARE @tblUserStatus AS TABLE (
		[ID] [bigint] NOT NULL,
		[StatusName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblUserStatus
	SELECT 1, 'New'	UNION 
	SELECT 2, 'Activated'	UNION
	SELECT 3, 'Blocked'		UNION
	SELECT 4, 'Deleted'		

	SET IDENTITY_INSERT dbo.UserStatus ON;

	MERGE dbo.UserStatus AS t
	USING @tblUserStatus AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[StatusName] = s.[StatusName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[StatusName]) VALUES (s.[ID], s.[StatusName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.UserStatus OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_UserType_Populate'))
   DROP PROC dbo.p_UserType_Populate
GO

CREATE PROCEDURE dbo.p_UserType_Populate 
	
AS
BEGIN

	DECLARE @tblUserType AS TABLE (
		[ID] [bigint] NOT NULL,
		[UserTypeName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblUserType
	SELECT 1, 'Customer'	UNION 
	SELECT 2, 'Manager'		UNION
	SELECT 3, 'Admin'		UNION
	SELECT 4, 'System'		UNION
	SELECT 5, 'Partner'		

	SET IDENTITY_INSERT dbo.UserType ON;

	MERGE dbo.UserType AS t
	USING @tblUserType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[UserTypeName] = s.[UserTypeName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[UserTypeName]) VALUES (s.[ID], s.[UserTypeName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.UserType OFF;
	
	SET NOCOUNT ON;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_Utils_EraseDeletedData'))
   DROP PROC dbo.p_Utils_EraseDeletedData
GO

/*
Usage:
EXEC dbo.p_Utils_EraseDeletedData
*/
CREATE PROCEDURE dbo.p_Utils_EraseDeletedData 
	
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @tblParams AS TABLE (
		[Order] INT,
		[Table] NVARCHAR(100)
	)

	DECLARE @table AS NVARCHAR(100) 

	INSERT INTO @tblParams	
	

	SELECT 1, 'Address' 	UNION
	SELECT 2, 'AddressType' 	UNION
	SELECT 3, 'Category' 	UNION
	SELECT 4, 'City' 	UNION
	SELECT 5, 'Contact' 	UNION
	SELECT 6, 'ContactType' 	UNION
	SELECT 7, 'Country' 	UNION
	SELECT 8, 'Currency' 	UNION
	SELECT 9, 'DeliveryService' 	UNION
	SELECT 10, 'FrameType' 	UNION
	SELECT 11, 'Image' 	UNION
	SELECT 12, 'Mat' 	UNION
	SELECT 13, 'MaterialType' 	UNION
	SELECT 14, 'MountingType' 	UNION
	SELECT 15, 'Order' 	UNION
	SELECT 16, 'OrderItem' 	UNION
	SELECT 17, 'OrderPaymentDetails' 	UNION
	SELECT 18, 'OrderStatus' 	UNION
	SELECT 19, 'PaymentMethod' 	UNION
	SELECT 20, 'PrintingHouse' 	UNION
	SELECT 21, 'Region' 	UNION
	SELECT 22, 'Size' 	UNION
	SELECT 23, 'Unit' 	UNION
	SELECT 25, 'UserStatus' 	UNION
	SELECT 26, 'UserType' 	


	DECLARE paramsCursor CURSOR FOR
	SELECT [Table] FROM @tblParams ORDER BY [Order]

	DECLARE @sql AS NVARCHAR(MAX)
	
	OPEN paramsCursor 

	FETCH NEXT FROM paramsCursor INTO @table

	WHILE @@FETCH_STATUS = 0
	BEGIN

		BEGIN TRY

			BEGIN TRANSACTION

			PRINT('======= ' + @table + ' =======')

			SET @sql = 'DELETE FROM dbo.[' + @table + '] WHERE IsDeleted = 1'
			PRINT(@sql)

			EXEC(@sql);

			FETCH NEXT FROM paramsCursor INTO @table

			COMMIT TRANSACTION

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SELECT   
				ERROR_NUMBER() AS ErrorNumber  
			   ,ERROR_MESSAGE() AS ErrorMessage;
		END CATCH

	END

		

	CLOSE paramsCursor
	DEALLOCATE paramsCursor
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_Delete]
GO

CREATE PROCEDURE [dbo].[p_Address_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Address]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Address]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_Erase]
GO

CREATE PROCEDURE [dbo].[p_Address_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Address]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Address] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Address_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Address] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_GetByAddressTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_GetByAddressTypeID]
GO

CREATE PROCEDURE [dbo].[p_Address_GetByAddressTypeID]

	@AddressTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Address] c 
				WHERE
					[AddressTypeID] = @AddressTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Address] e
		WHERE 
			[AddressTypeID] = @AddressTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_GetByCityID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_GetByCityID]
GO

CREATE PROCEDURE [dbo].[p_Address_GetByCityID]

	@CityID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Address] c 
				WHERE
					[CityID] = @CityID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Address] e
		WHERE 
			[CityID] = @CityID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_Address_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Address] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Address] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_Address_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Address] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Address] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Address_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Address] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Address] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_Insert]
GO

CREATE PROCEDURE [dbo].[p_Address_Insert]
			@ID BIGINT,
			@AddressTypeID BIGINT,
			@Title NVARCHAR(50),
			@CityID BIGINT,
			@Street NVARCHAR(50),
			@BuildingNo NVARCHAR(50),
			@ApartmentNo NVARCHAR(50),
			@Comment NVARCHAR(1000),
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Address]
	SELECT 
		@AddressTypeID,
		@Title,
		@CityID,
		@Street,
		@BuildingNo,
		@ApartmentNo,
		@Comment,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[Address] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressTypeID IS NOT NULL THEN (CASE WHEN e.[AddressTypeID] = @AddressTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN e.[CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Street IS NOT NULL THEN (CASE WHEN e.[Street] = @Street THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @BuildingNo IS NOT NULL THEN (CASE WHEN e.[BuildingNo] = @BuildingNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ApartmentNo IS NOT NULL THEN (CASE WHEN e.[ApartmentNo] = @ApartmentNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_Update]
GO

CREATE PROCEDURE [dbo].[p_Address_Update]
			@ID BIGINT,
			@AddressTypeID BIGINT,
			@Title NVARCHAR(50),
			@CityID BIGINT,
			@Street NVARCHAR(50),
			@BuildingNo NVARCHAR(50),
			@ApartmentNo NVARCHAR(50),
			@Comment NVARCHAR(1000),
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Address]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Address]
		SET
									[AddressTypeID] = IIF( @AddressTypeID IS NOT NULL, @AddressTypeID, [AddressTypeID] ) ,
									[Title] = IIF( @Title IS NOT NULL, @Title, [Title] ) ,
									[CityID] = IIF( @CityID IS NOT NULL, @CityID, [CityID] ) ,
									[Street] = IIF( @Street IS NOT NULL, @Street, [Street] ) ,
									[BuildingNo] = IIF( @BuildingNo IS NOT NULL, @BuildingNo, [BuildingNo] ) ,
									[ApartmentNo] = IIF( @ApartmentNo IS NOT NULL, @ApartmentNo, [ApartmentNo] ) ,
									[Comment] = IIF( @Comment IS NOT NULL, @Comment, [Comment] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Address was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Address] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressTypeID IS NOT NULL THEN (CASE WHEN e.[AddressTypeID] = @AddressTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN e.[CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Street IS NOT NULL THEN (CASE WHEN e.[Street] = @Street THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @BuildingNo IS NOT NULL THEN (CASE WHEN e.[BuildingNo] = @BuildingNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ApartmentNo IS NOT NULL THEN (CASE WHEN e.[ApartmentNo] = @ApartmentNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_Delete]
GO

CREATE PROCEDURE [dbo].[p_AddressType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[AddressType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[AddressType]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_Erase]
GO

CREATE PROCEDURE [dbo].[p_AddressType_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[AddressType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[AddressType] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_AddressType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[AddressType] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_AddressType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[AddressType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[AddressType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_Insert]
GO

CREATE PROCEDURE [dbo].[p_AddressType_Insert]
			@ID BIGINT,
			@AddressTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[AddressType]
	SELECT 
		@AddressTypeName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[AddressType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressTypeName IS NOT NULL THEN (CASE WHEN e.[AddressTypeName] = @AddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_Update]
GO

CREATE PROCEDURE [dbo].[p_AddressType_Update]
			@ID BIGINT,
			@AddressTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[AddressType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[AddressType]
		SET
									[AddressTypeName] = IIF( @AddressTypeName IS NOT NULL, @AddressTypeName, [AddressTypeName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'AddressType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[AddressType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressTypeName IS NOT NULL THEN (CASE WHEN e.[AddressTypeName] = @AddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_Delete]
GO

CREATE PROCEDURE [dbo].[p_Category_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Category]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Category]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_Erase]
GO

CREATE PROCEDURE [dbo].[p_Category_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Category]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Category] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Category_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Category] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_Category_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Category] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Category] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_Category_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Category] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Category] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_GetByParentID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_GetByParentID]
GO

CREATE PROCEDURE [dbo].[p_Category_GetByParentID]

	@ParentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Category] c 
				WHERE
					[ParentID] = @ParentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Category] e
		WHERE 
			[ParentID] = @ParentID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Category_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Category] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Category] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_Insert]
GO

CREATE PROCEDURE [dbo].[p_Category_Insert]
			@ID BIGINT,
			@CategoryName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ParentID BIGINT,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Category]
	SELECT 
		@CategoryName,
		@Description,
		@ParentID,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Category] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CategoryName IS NOT NULL THEN (CASE WHEN e.[CategoryName] = @CategoryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN e.[ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_Update]
GO

CREATE PROCEDURE [dbo].[p_Category_Update]
			@ID BIGINT,
			@CategoryName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ParentID BIGINT,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Category]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Category]
		SET
									[CategoryName] = IIF( @CategoryName IS NOT NULL, @CategoryName, [CategoryName] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[ParentID] = IIF( @ParentID IS NOT NULL, @ParentID, [ParentID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Category was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Category] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CategoryName IS NOT NULL THEN (CASE WHEN e.[CategoryName] = @CategoryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN e.[ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_Delete]
GO

CREATE PROCEDURE [dbo].[p_City_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[City]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[City]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_Erase]
GO

CREATE PROCEDURE [dbo].[p_City_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[City]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[City] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_GetAll]
GO

CREATE PROCEDURE [dbo].[p_City_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[City] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_GetByRegionID', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_GetByRegionID]
GO

CREATE PROCEDURE [dbo].[p_City_GetByRegionID]

	@RegionID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[City] c 
				WHERE
					[RegionID] = @RegionID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[City] e
		WHERE 
			[RegionID] = @RegionID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_City_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[City] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[City] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_Insert]
GO

CREATE PROCEDURE [dbo].[p_City_Insert]
			@ID BIGINT,
			@CityName NVARCHAR(250),
			@RegionID BIGINT,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[City]
	SELECT 
		@CityName,
		@RegionID,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[City] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityName IS NOT NULL THEN (CASE WHEN e.[CityName] = @CityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RegionID IS NOT NULL THEN (CASE WHEN e.[RegionID] = @RegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_Update]
GO

CREATE PROCEDURE [dbo].[p_City_Update]
			@ID BIGINT,
			@CityName NVARCHAR(250),
			@RegionID BIGINT,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[City]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[City]
		SET
									[CityName] = IIF( @CityName IS NOT NULL, @CityName, [CityName] ) ,
									[RegionID] = IIF( @RegionID IS NOT NULL, @RegionID, [RegionID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'City was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[City] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityName IS NOT NULL THEN (CASE WHEN e.[CityName] = @CityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RegionID IS NOT NULL THEN (CASE WHEN e.[RegionID] = @RegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_Delete]
GO

CREATE PROCEDURE [dbo].[p_Contact_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Contact]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Contact]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_Erase]
GO

CREATE PROCEDURE [dbo].[p_Contact_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Contact]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Contact] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Contact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Contact] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_GetByContactTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_GetByContactTypeID]
GO

CREATE PROCEDURE [dbo].[p_Contact_GetByContactTypeID]

	@ContactTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Contact] c 
				WHERE
					[ContactTypeID] = @ContactTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Contact] e
		WHERE 
			[ContactTypeID] = @ContactTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_Contact_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Contact] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Contact] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_Contact_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Contact] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Contact] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Contact_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Contact] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Contact] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_Insert]
GO

CREATE PROCEDURE [dbo].[p_Contact_Insert]
			@ID BIGINT,
			@ContactTypeID BIGINT,
			@Title NVARCHAR(50),
			@Comment NVARCHAR(250),
			@Value NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Contact]
	SELECT 
		@ContactTypeID,
		@Title,
		@Comment,
		@Value,
		@IsDeleted,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Contact] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactTypeID IS NOT NULL THEN (CASE WHEN e.[ContactTypeID] = @ContactTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN e.[Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_Update]
GO

CREATE PROCEDURE [dbo].[p_Contact_Update]
			@ID BIGINT,
			@ContactTypeID BIGINT,
			@Title NVARCHAR(50),
			@Comment NVARCHAR(250),
			@Value NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Contact]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Contact]
		SET
									[ContactTypeID] = IIF( @ContactTypeID IS NOT NULL, @ContactTypeID, [ContactTypeID] ) ,
									[Title] = IIF( @Title IS NOT NULL, @Title, [Title] ) ,
									[Comment] = IIF( @Comment IS NOT NULL, @Comment, [Comment] ) ,
									[Value] = IIF( @Value IS NOT NULL, @Value, [Value] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Contact was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Contact] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactTypeID IS NOT NULL THEN (CASE WHEN e.[ContactTypeID] = @ContactTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN e.[Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ContactType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_ContactType_Delete]
GO

CREATE PROCEDURE [dbo].[p_ContactType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ContactType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[ContactType]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ContactType_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_ContactType_Erase]
GO

CREATE PROCEDURE [dbo].[p_ContactType_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ContactType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[ContactType] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ContactType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ContactType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ContactType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ContactType] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ContactType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_ContactType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_ContactType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ContactType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ContactType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ContactType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ContactType_Insert]
GO

CREATE PROCEDURE [dbo].[p_ContactType_Insert]
			@ID BIGINT,
			@ContactTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ContactType]
	SELECT 
		@ContactTypeName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[ContactType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN e.[ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ContactType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ContactType_Update]
GO

CREATE PROCEDURE [dbo].[p_ContactType_Update]
			@ID BIGINT,
			@ContactTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ContactType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ContactType]
		SET
									[ContactTypeName] = IIF( @ContactTypeName IS NOT NULL, @ContactTypeName, [ContactTypeName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ContactType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ContactType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN e.[ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Country_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Country_Delete]
GO

CREATE PROCEDURE [dbo].[p_Country_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Country]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Country]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Country_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Country_Erase]
GO

CREATE PROCEDURE [dbo].[p_Country_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Country]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Country] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Country_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Country_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Country_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Country] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Country_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Country_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Country_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Country] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Country] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Country_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Country_Insert]
GO

CREATE PROCEDURE [dbo].[p_Country_Insert]
			@ID BIGINT,
			@CountryName NVARCHAR(50),
			@ISO NVARCHAR(5),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Country]
	SELECT 
		@CountryName,
		@ISO,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[Country] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CountryName IS NOT NULL THEN (CASE WHEN e.[CountryName] = @CountryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN e.[ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Country_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Country_Update]
GO

CREATE PROCEDURE [dbo].[p_Country_Update]
			@ID BIGINT,
			@CountryName NVARCHAR(50),
			@ISO NVARCHAR(5),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Country]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Country]
		SET
									[CountryName] = IIF( @CountryName IS NOT NULL, @CountryName, [CountryName] ) ,
									[ISO] = IIF( @ISO IS NOT NULL, @ISO, [ISO] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Country was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Country] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CountryName IS NOT NULL THEN (CASE WHEN e.[CountryName] = @CountryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN e.[ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_Delete]
GO

CREATE PROCEDURE [dbo].[p_Currency_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Currency]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Currency]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_Erase]
GO

CREATE PROCEDURE [dbo].[p_Currency_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Currency]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Currency] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Currency_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Currency] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Currency_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Currency] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Currency] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_Insert]
GO

CREATE PROCEDURE [dbo].[p_Currency_Insert]
			@ID BIGINT,
			@ISO NVARCHAR(5),
			@CurrencyName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Currency]
	SELECT 
		@ISO,
		@CurrencyName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[Currency] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN e.[ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CurrencyName IS NOT NULL THEN (CASE WHEN e.[CurrencyName] = @CurrencyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_Update]
GO

CREATE PROCEDURE [dbo].[p_Currency_Update]
			@ID BIGINT,
			@ISO NVARCHAR(5),
			@CurrencyName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Currency]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Currency]
		SET
									[ISO] = IIF( @ISO IS NOT NULL, @ISO, [ISO] ) ,
									[CurrencyName] = IIF( @CurrencyName IS NOT NULL, @CurrencyName, [CurrencyName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Currency was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Currency] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN e.[ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CurrencyName IS NOT NULL THEN (CASE WHEN e.[CurrencyName] = @CurrencyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_Delete]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[DeliveryService]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[DeliveryService]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_Erase]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[DeliveryService]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[DeliveryService] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_GetAll]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[DeliveryService] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryService] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DeliveryService] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryService] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DeliveryService] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryService] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DeliveryService] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_Insert]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_Insert]
			@ID BIGINT,
			@DeliveryServiceName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[DeliveryService]
	SELECT 
		@DeliveryServiceName,
		@Description,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[DeliveryService] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeliveryServiceName IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceName] = @DeliveryServiceName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_Update]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_Update]
			@ID BIGINT,
			@DeliveryServiceName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryService]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[DeliveryService]
		SET
									[DeliveryServiceName] = IIF( @DeliveryServiceName IS NOT NULL, @DeliveryServiceName, [DeliveryServiceName] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'DeliveryService was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[DeliveryService] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeliveryServiceName IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceName] = @DeliveryServiceName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_Delete]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_Delete]
		@DeliveryServiceID BIGINT,	
		@CityID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[DeliveryServiceCity]  
				WHERE 
							[DeliveryServiceID] = @DeliveryServiceID	AND
							[CityID] = @CityID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[DeliveryServiceCity] 
			WHERE 
						[DeliveryServiceID] = @DeliveryServiceID	AND
						[CityID] = @CityID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_GetAll]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[DeliveryServiceCity] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_GetByCityID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_GetByCityID]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_GetByCityID]

	@CityID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryServiceCity] c 
				WHERE
					[CityID] = @CityID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DeliveryServiceCity] e
		WHERE 
			[CityID] = @CityID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_GetByDeliveryServiceID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_GetByDeliveryServiceID]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_GetByDeliveryServiceID]

	@DeliveryServiceID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryServiceCity] c 
				WHERE
					[DeliveryServiceID] = @DeliveryServiceID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DeliveryServiceCity] e
		WHERE 
			[DeliveryServiceID] = @DeliveryServiceID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_GetDetails]
		@DeliveryServiceID BIGINT,	
		@CityID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryServiceCity] c 
				WHERE 
								[DeliveryServiceID] = @DeliveryServiceID	AND
								[CityID] = @CityID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DeliveryServiceCity] e
		WHERE 
								[DeliveryServiceID] = @DeliveryServiceID	AND
								[CityID] = @CityID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_Insert]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_Insert]
			@DeliveryServiceID BIGINT,
			@CityID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[DeliveryServiceCity]
	SELECT 
		@DeliveryServiceID,
		@CityID
	
	

	SELECT
		e.*
	FROM
		[dbo].[DeliveryServiceCity] e
	WHERE
				(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN e.[CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_Update]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_Update]
			@DeliveryServiceID BIGINT,
			@CityID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryServiceCity]
					WHERE 
												[DeliveryServiceID] = @DeliveryServiceID	AND
												[CityID] = @CityID	
							))
	BEGIN
		UPDATE [dbo].[DeliveryServiceCity]
		SET
									[DeliveryServiceID] = IIF( @DeliveryServiceID IS NOT NULL, @DeliveryServiceID, [DeliveryServiceID] ) ,
									[CityID] = IIF( @CityID IS NOT NULL, @CityID, [CityID] ) 
						WHERE 
												[DeliveryServiceID] = @DeliveryServiceID	AND
												[CityID] = @CityID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'DeliveryServiceCity was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[DeliveryServiceCity] e
	WHERE
				(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN e.[CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_Delete]
GO

CREATE PROCEDURE [dbo].[p_FrameType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[FrameType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[FrameType]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_Erase]
GO

CREATE PROCEDURE [dbo].[p_FrameType_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[FrameType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[FrameType] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_FrameType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[FrameType] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_FrameType_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[FrameType] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[FrameType] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_FrameType_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[FrameType] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[FrameType] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_FrameType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[FrameType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[FrameType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_Insert]
GO

CREATE PROCEDURE [dbo].[p_FrameType_Insert]
			@ID BIGINT,
			@FrameTypeName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[FrameType]
	SELECT 
		@FrameTypeName,
		@Description,
		@ThumbnailUrl,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[FrameType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameTypeName IS NOT NULL THEN (CASE WHEN e.[FrameTypeName] = @FrameTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_Update]
GO

CREATE PROCEDURE [dbo].[p_FrameType_Update]
			@ID BIGINT,
			@FrameTypeName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[FrameType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[FrameType]
		SET
									[FrameTypeName] = IIF( @FrameTypeName IS NOT NULL, @FrameTypeName, [FrameTypeName] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[ThumbnailUrl] = IIF( @ThumbnailUrl IS NOT NULL, @ThumbnailUrl, [ThumbnailUrl] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'FrameType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[FrameType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameTypeName IS NOT NULL THEN (CASE WHEN e.[FrameTypeName] = @FrameTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_Delete]
GO

CREATE PROCEDURE [dbo].[p_Image_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Image]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Image]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_Erase]
GO

CREATE PROCEDURE [dbo].[p_Image_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Image]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Image] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Image_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Image] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_Image_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Image] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Image] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_Image_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Image] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Image] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_GetByPriceCurrencyID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_GetByPriceCurrencyID]
GO

CREATE PROCEDURE [dbo].[p_Image_GetByPriceCurrencyID]

	@PriceCurrencyID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Image] c 
				WHERE
					[PriceCurrencyID] = @PriceCurrencyID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Image] e
		WHERE 
			[PriceCurrencyID] = @PriceCurrencyID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Image_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Image] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Image] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_Insert]
GO

CREATE PROCEDURE [dbo].[p_Image_Insert]
			@ID BIGINT,
			@Title NVARCHAR(50),
			@Description NVARCHAR(1000),
			@OriginUrl NVARCHAR(3000),
			@MaxWidth INT,
			@MaxHeight INT,
			@PriceAmount DECIMAL(18, 2),
			@PriceCurrencyID BIGINT,
			@IsDeleted BIT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Image]
	SELECT 
		@Title,
		@Description,
		@OriginUrl,
		@MaxWidth,
		@MaxHeight,
		@PriceAmount,
		@PriceCurrencyID,
		@IsDeleted,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Image] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OriginUrl IS NOT NULL THEN (CASE WHEN e.[OriginUrl] = @OriginUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaxWidth IS NOT NULL THEN (CASE WHEN e.[MaxWidth] = @MaxWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaxHeight IS NOT NULL THEN (CASE WHEN e.[MaxHeight] = @MaxHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceAmount IS NOT NULL THEN (CASE WHEN e.[PriceAmount] = @PriceAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN e.[PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_Update]
GO

CREATE PROCEDURE [dbo].[p_Image_Update]
			@ID BIGINT,
			@Title NVARCHAR(50),
			@Description NVARCHAR(1000),
			@OriginUrl NVARCHAR(3000),
			@MaxWidth INT,
			@MaxHeight INT,
			@PriceAmount DECIMAL(18, 2),
			@PriceCurrencyID BIGINT,
			@IsDeleted BIT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Image]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Image]
		SET
									[Title] = IIF( @Title IS NOT NULL, @Title, [Title] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[OriginUrl] = IIF( @OriginUrl IS NOT NULL, @OriginUrl, [OriginUrl] ) ,
									[MaxWidth] = IIF( @MaxWidth IS NOT NULL, @MaxWidth, [MaxWidth] ) ,
									[MaxHeight] = IIF( @MaxHeight IS NOT NULL, @MaxHeight, [MaxHeight] ) ,
									[PriceAmount] = IIF( @PriceAmount IS NOT NULL, @PriceAmount, [PriceAmount] ) ,
									[PriceCurrencyID] = IIF( @PriceCurrencyID IS NOT NULL, @PriceCurrencyID, [PriceCurrencyID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Image was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Image] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OriginUrl IS NOT NULL THEN (CASE WHEN e.[OriginUrl] = @OriginUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaxWidth IS NOT NULL THEN (CASE WHEN e.[MaxWidth] = @MaxWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaxHeight IS NOT NULL THEN (CASE WHEN e.[MaxHeight] = @MaxHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceAmount IS NOT NULL THEN (CASE WHEN e.[PriceAmount] = @PriceAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN e.[PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_Delete]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_Delete]
		@ImageID BIGINT,	
		@CategoryID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ImageCategory]  
				WHERE 
							[ImageID] = @ImageID	AND
							[CategoryID] = @CategoryID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[ImageCategory] 
			WHERE 
						[ImageID] = @ImageID	AND
						[CategoryID] = @CategoryID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageCategory] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_GetByCategoryID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_GetByCategoryID]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_GetByCategoryID]

	@CategoryID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageCategory] c 
				WHERE
					[CategoryID] = @CategoryID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageCategory] e
		WHERE 
			[CategoryID] = @CategoryID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_GetByImageID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_GetByImageID]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_GetByImageID]

	@ImageID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageCategory] c 
				WHERE
					[ImageID] = @ImageID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageCategory] e
		WHERE 
			[ImageID] = @ImageID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_GetDetails]
		@ImageID BIGINT,	
		@CategoryID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageCategory] c 
				WHERE 
								[ImageID] = @ImageID	AND
								[CategoryID] = @CategoryID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageCategory] e
		WHERE 
								[ImageID] = @ImageID	AND
								[CategoryID] = @CategoryID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_Insert]
			@ImageID BIGINT,
			@CategoryID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImageCategory]
	SELECT 
		@ImageID,
		@CategoryID
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImageCategory] e
	WHERE
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN e.[CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_Update]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_Update]
			@ImageID BIGINT,
			@CategoryID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageCategory]
					WHERE 
												[ImageID] = @ImageID	AND
												[CategoryID] = @CategoryID	
							))
	BEGIN
		UPDATE [dbo].[ImageCategory]
		SET
									[ImageID] = IIF( @ImageID IS NOT NULL, @ImageID, [ImageID] ) ,
									[CategoryID] = IIF( @CategoryID IS NOT NULL, @CategoryID, [CategoryID] ) 
						WHERE 
												[ImageID] = @ImageID	AND
												[CategoryID] = @CategoryID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImageCategory was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImageCategory] e
	WHERE
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN e.[CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_Delete]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_Delete]
		@ImageID BIGINT,	
		@RelatedImageID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ImageRelated]  
				WHERE 
							[ImageID] = @ImageID	AND
							[RelatedImageID] = @RelatedImageID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[ImageRelated] 
			WHERE 
						[ImageID] = @ImageID	AND
						[RelatedImageID] = @RelatedImageID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageRelated] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_GetByImageID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_GetByImageID]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_GetByImageID]

	@ImageID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageRelated] c 
				WHERE
					[ImageID] = @ImageID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageRelated] e
		WHERE 
			[ImageID] = @ImageID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_GetByRelatedImageID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_GetByRelatedImageID]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_GetByRelatedImageID]

	@RelatedImageID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageRelated] c 
				WHERE
					[RelatedImageID] = @RelatedImageID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageRelated] e
		WHERE 
			[RelatedImageID] = @RelatedImageID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_GetDetails]
		@ImageID BIGINT,	
		@RelatedImageID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageRelated] c 
				WHERE 
								[ImageID] = @ImageID	AND
								[RelatedImageID] = @RelatedImageID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageRelated] e
		WHERE 
								[ImageID] = @ImageID	AND
								[RelatedImageID] = @RelatedImageID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_Insert]
			@ImageID BIGINT,
			@RelatedImageID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImageRelated]
	SELECT 
		@ImageID,
		@RelatedImageID
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImageRelated] e
	WHERE
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN e.[RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_Update]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_Update]
			@ImageID BIGINT,
			@RelatedImageID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageRelated]
					WHERE 
												[ImageID] = @ImageID	AND
												[RelatedImageID] = @RelatedImageID	
							))
	BEGIN
		UPDATE [dbo].[ImageRelated]
		SET
									[ImageID] = IIF( @ImageID IS NOT NULL, @ImageID, [ImageID] ) ,
									[RelatedImageID] = IIF( @RelatedImageID IS NOT NULL, @RelatedImageID, [RelatedImageID] ) 
						WHERE 
												[ImageID] = @ImageID	AND
												[RelatedImageID] = @RelatedImageID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImageRelated was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImageRelated] e
	WHERE
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN e.[RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageThumbnail_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageThumbnail_Delete]
GO

CREATE PROCEDURE [dbo].[p_ImageThumbnail_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ImageThumbnail]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[ImageThumbnail] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageThumbnail_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageThumbnail_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImageThumbnail_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageThumbnail] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageThumbnail_GetByImageID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageThumbnail_GetByImageID]
GO

CREATE PROCEDURE [dbo].[p_ImageThumbnail_GetByImageID]

	@ImageID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageThumbnail] c 
				WHERE
					[ImageID] = @ImageID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageThumbnail] e
		WHERE 
			[ImageID] = @ImageID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageThumbnail_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageThumbnail_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_ImageThumbnail_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageThumbnail] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageThumbnail] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageThumbnail_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageThumbnail_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImageThumbnail_Insert]
			@ID BIGINT,
			@Url NVARCHAR(1000),
			@Order INT,
			@ImageID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImageThumbnail]
	SELECT 
		@Url,
		@Order,
		@ImageID
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImageThumbnail] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN e.[Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN e.[Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageThumbnail_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageThumbnail_Update]
GO

CREATE PROCEDURE [dbo].[p_ImageThumbnail_Update]
			@ID BIGINT,
			@Url NVARCHAR(1000),
			@Order INT,
			@ImageID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageThumbnail]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ImageThumbnail]
		SET
									[Url] = IIF( @Url IS NOT NULL, @Url, [Url] ) ,
									[Order] = IIF( @Order IS NOT NULL, @Order, [Order] ) ,
									[ImageID] = IIF( @ImageID IS NOT NULL, @ImageID, [ImageID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImageThumbnail was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImageThumbnail] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN e.[Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN e.[Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Mat_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Mat_Delete]
GO

CREATE PROCEDURE [dbo].[p_Mat_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Mat]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Mat]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Mat_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Mat_Erase]
GO

CREATE PROCEDURE [dbo].[p_Mat_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Mat]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Mat] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Mat_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Mat_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Mat_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Mat] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Mat_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Mat_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_Mat_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Mat] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Mat] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Mat_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Mat_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_Mat_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Mat] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Mat] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Mat_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Mat_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Mat_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Mat] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Mat] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Mat_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Mat_Insert]
GO

CREATE PROCEDURE [dbo].[p_Mat_Insert]
			@ID BIGINT,
			@MatName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Mat]
	SELECT 
		@MatName,
		@Description,
		@ThumbnailUrl,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Mat] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MatName IS NOT NULL THEN (CASE WHEN e.[MatName] = @MatName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Mat_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Mat_Update]
GO

CREATE PROCEDURE [dbo].[p_Mat_Update]
			@ID BIGINT,
			@MatName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Mat]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Mat]
		SET
									[MatName] = IIF( @MatName IS NOT NULL, @MatName, [MatName] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[ThumbnailUrl] = IIF( @ThumbnailUrl IS NOT NULL, @ThumbnailUrl, [ThumbnailUrl] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Mat was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Mat] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MatName IS NOT NULL THEN (CASE WHEN e.[MatName] = @MatName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MaterialType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_MaterialType_Delete]
GO

CREATE PROCEDURE [dbo].[p_MaterialType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[MaterialType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[MaterialType]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MaterialType_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_MaterialType_Erase]
GO

CREATE PROCEDURE [dbo].[p_MaterialType_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[MaterialType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[MaterialType] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MaterialType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_MaterialType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_MaterialType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[MaterialType] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MaterialType_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_MaterialType_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_MaterialType_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[MaterialType] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[MaterialType] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MaterialType_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_MaterialType_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_MaterialType_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[MaterialType] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[MaterialType] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MaterialType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_MaterialType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_MaterialType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[MaterialType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[MaterialType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MaterialType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_MaterialType_Insert]
GO

CREATE PROCEDURE [dbo].[p_MaterialType_Insert]
			@ID BIGINT,
			@MaterialTypeName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[MaterialType]
	SELECT 
		@MaterialTypeName,
		@Description,
		@ThumbnailUrl,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[MaterialType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaterialTypeName IS NOT NULL THEN (CASE WHEN e.[MaterialTypeName] = @MaterialTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MaterialType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_MaterialType_Update]
GO

CREATE PROCEDURE [dbo].[p_MaterialType_Update]
			@ID BIGINT,
			@MaterialTypeName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[MaterialType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[MaterialType]
		SET
									[MaterialTypeName] = IIF( @MaterialTypeName IS NOT NULL, @MaterialTypeName, [MaterialTypeName] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[ThumbnailUrl] = IIF( @ThumbnailUrl IS NOT NULL, @ThumbnailUrl, [ThumbnailUrl] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'MaterialType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[MaterialType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaterialTypeName IS NOT NULL THEN (CASE WHEN e.[MaterialTypeName] = @MaterialTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MountingType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_MountingType_Delete]
GO

CREATE PROCEDURE [dbo].[p_MountingType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[MountingType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[MountingType]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MountingType_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_MountingType_Erase]
GO

CREATE PROCEDURE [dbo].[p_MountingType_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[MountingType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[MountingType] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MountingType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_MountingType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_MountingType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[MountingType] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MountingType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_MountingType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_MountingType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[MountingType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[MountingType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MountingType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_MountingType_Insert]
GO

CREATE PROCEDURE [dbo].[p_MountingType_Insert]
			@ID BIGINT,
			@MountingTypeName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[MountingType]
	SELECT 
		@MountingTypeName,
		@Description,
		@ThumbnailUrl,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[MountingType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MountingTypeName IS NOT NULL THEN (CASE WHEN e.[MountingTypeName] = @MountingTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MountingType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_MountingType_Update]
GO

CREATE PROCEDURE [dbo].[p_MountingType_Update]
			@ID BIGINT,
			@MountingTypeName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[MountingType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[MountingType]
		SET
									[MountingTypeName] = IIF( @MountingTypeName IS NOT NULL, @MountingTypeName, [MountingTypeName] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[ThumbnailUrl] = IIF( @ThumbnailUrl IS NOT NULL, @ThumbnailUrl, [ThumbnailUrl] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'MountingType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[MountingType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MountingTypeName IS NOT NULL THEN (CASE WHEN e.[MountingTypeName] = @MountingTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_Delete]
GO

CREATE PROCEDURE [dbo].[p_Order_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Order]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Order]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_Erase]
GO

CREATE PROCEDURE [dbo].[p_Order_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Order]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Order] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Order_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Order] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByContactID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByContactID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByContactID]

	@ContactID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[ContactID] = @ContactID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[ContactID] = @ContactID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByDeliveryAddressID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByDeliveryAddressID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByDeliveryAddressID]

	@DeliveryAddressID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[DeliveryAddressID] = @DeliveryAddressID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[DeliveryAddressID] = @DeliveryAddressID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByDeliveryServiceID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByDeliveryServiceID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByDeliveryServiceID]

	@DeliveryServiceID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[DeliveryServiceID] = @DeliveryServiceID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[DeliveryServiceID] = @DeliveryServiceID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByManagerID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByManagerID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByManagerID]

	@ManagerID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[ManagerID] = @ManagerID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[ManagerID] = @ManagerID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByUserID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByUserID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Order_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_Insert]
GO

CREATE PROCEDURE [dbo].[p_Order_Insert]
			@ID BIGINT,
			@ManagerID BIGINT,
			@UserID BIGINT,
			@ContactID BIGINT,
			@DeliveryAddressID BIGINT,
			@DeliveryServiceID BIGINT,
			@Comments NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Order]
	SELECT 
		@ManagerID,
		@UserID,
		@ContactID,
		@DeliveryAddressID,
		@DeliveryServiceID,
		@Comments,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Order] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN e.[ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeliveryAddressID IS NOT NULL THEN (CASE WHEN e.[DeliveryAddressID] = @DeliveryAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN e.[Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_Update]
GO

CREATE PROCEDURE [dbo].[p_Order_Update]
			@ID BIGINT,
			@ManagerID BIGINT,
			@UserID BIGINT,
			@ContactID BIGINT,
			@DeliveryAddressID BIGINT,
			@DeliveryServiceID BIGINT,
			@Comments NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Order]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Order]
		SET
									[ManagerID] = IIF( @ManagerID IS NOT NULL, @ManagerID, [ManagerID] ) ,
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[ContactID] = IIF( @ContactID IS NOT NULL, @ContactID, [ContactID] ) ,
									[DeliveryAddressID] = IIF( @DeliveryAddressID IS NOT NULL, @DeliveryAddressID, [DeliveryAddressID] ) ,
									[DeliveryServiceID] = IIF( @DeliveryServiceID IS NOT NULL, @DeliveryServiceID, [DeliveryServiceID] ) ,
									[Comments] = IIF( @Comments IS NOT NULL, @Comments, [Comments] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Order was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Order] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN e.[ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeliveryAddressID IS NOT NULL THEN (CASE WHEN e.[DeliveryAddressID] = @DeliveryAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN e.[Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_Delete]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderItem]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[OrderItem]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_Erase]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderItem]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[OrderItem] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderItem] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByFrameSizeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByFrameSizeID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByFrameSizeID]

	@FrameSizeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[FrameSizeID] = @FrameSizeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[FrameSizeID] = @FrameSizeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByFrameTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByFrameTypeID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByFrameTypeID]

	@FrameTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[FrameTypeID] = @FrameTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[FrameTypeID] = @FrameTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByImageID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByImageID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByImageID]

	@ImageID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[ImageID] = @ImageID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[ImageID] = @ImageID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByMaterialTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByMaterialTypeID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByMaterialTypeID]

	@MaterialTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[MaterialTypeID] = @MaterialTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[MaterialTypeID] = @MaterialTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByMatID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByMatID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByMatID]

	@MatID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[MatID] = @MatID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[MatID] = @MatID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByMountingTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByMountingTypeID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByMountingTypeID]

	@MountingTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[MountingTypeID] = @MountingTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[MountingTypeID] = @MountingTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByOrderID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByOrderID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByOrderID]

	@OrderID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[OrderID] = @OrderID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[OrderID] = @OrderID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByPriceCurrencyID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByPriceCurrencyID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByPriceCurrencyID]

	@PriceCurrencyID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[PriceCurrencyID] = @PriceCurrencyID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[PriceCurrencyID] = @PriceCurrencyID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByPrintingHouseID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByPrintingHouseID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByPrintingHouseID]

	@PrintingHouseID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[PrintingHouseID] = @PrintingHouseID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[PrintingHouseID] = @PrintingHouseID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetBySizeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetBySizeID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetBySizeID]

	@SizeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[SizeID] = @SizeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[SizeID] = @SizeID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_Insert]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_Insert]
			@ID BIGINT,
			@OrderID BIGINT,
			@ImageID BIGINT,
			@Width INT,
			@Height INT,
			@SizeID BIGINT,
			@FrameTypeID BIGINT,
			@FrameSizeID BIGINT,
			@MatID BIGINT,
			@MaterialTypeID BIGINT,
			@MountingTypeID BIGINT,
			@ItemCount INT,
			@PriceAmountPerItem DECIMAL(18, 2),
			@PriceCurrencyID BIGINT,
			@Comments NVARCHAR(1000),
			@PrintingHouseID BIGINT,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderItem]
	SELECT 
		@OrderID,
		@ImageID,
		@Width,
		@Height,
		@SizeID,
		@FrameTypeID,
		@FrameSizeID,
		@MatID,
		@MaterialTypeID,
		@MountingTypeID,
		@ItemCount,
		@PriceAmountPerItem,
		@PriceCurrencyID,
		@Comments,
		@PrintingHouseID,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderItem] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN e.[Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN e.[Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SizeID IS NOT NULL THEN (CASE WHEN e.[SizeID] = @SizeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameTypeID IS NOT NULL THEN (CASE WHEN e.[FrameTypeID] = @FrameTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameSizeID IS NOT NULL THEN (CASE WHEN e.[FrameSizeID] = @FrameSizeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MatID IS NOT NULL THEN (CASE WHEN e.[MatID] = @MatID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaterialTypeID IS NOT NULL THEN (CASE WHEN e.[MaterialTypeID] = @MaterialTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MountingTypeID IS NOT NULL THEN (CASE WHEN e.[MountingTypeID] = @MountingTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ItemCount IS NOT NULL THEN (CASE WHEN e.[ItemCount] = @ItemCount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceAmountPerItem IS NOT NULL THEN (CASE WHEN e.[PriceAmountPerItem] = @PriceAmountPerItem THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN e.[PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN e.[Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_Update]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_Update]
			@ID BIGINT,
			@OrderID BIGINT,
			@ImageID BIGINT,
			@Width INT,
			@Height INT,
			@SizeID BIGINT,
			@FrameTypeID BIGINT,
			@FrameSizeID BIGINT,
			@MatID BIGINT,
			@MaterialTypeID BIGINT,
			@MountingTypeID BIGINT,
			@ItemCount INT,
			@PriceAmountPerItem DECIMAL(18, 2),
			@PriceCurrencyID BIGINT,
			@Comments NVARCHAR(1000),
			@PrintingHouseID BIGINT,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OrderItem]
		SET
									[OrderID] = IIF( @OrderID IS NOT NULL, @OrderID, [OrderID] ) ,
									[ImageID] = IIF( @ImageID IS NOT NULL, @ImageID, [ImageID] ) ,
									[Width] = IIF( @Width IS NOT NULL, @Width, [Width] ) ,
									[Height] = IIF( @Height IS NOT NULL, @Height, [Height] ) ,
									[SizeID] = IIF( @SizeID IS NOT NULL, @SizeID, [SizeID] ) ,
									[FrameTypeID] = IIF( @FrameTypeID IS NOT NULL, @FrameTypeID, [FrameTypeID] ) ,
									[FrameSizeID] = IIF( @FrameSizeID IS NOT NULL, @FrameSizeID, [FrameSizeID] ) ,
									[MatID] = IIF( @MatID IS NOT NULL, @MatID, [MatID] ) ,
									[MaterialTypeID] = IIF( @MaterialTypeID IS NOT NULL, @MaterialTypeID, [MaterialTypeID] ) ,
									[MountingTypeID] = IIF( @MountingTypeID IS NOT NULL, @MountingTypeID, [MountingTypeID] ) ,
									[ItemCount] = IIF( @ItemCount IS NOT NULL, @ItemCount, [ItemCount] ) ,
									[PriceAmountPerItem] = IIF( @PriceAmountPerItem IS NOT NULL, @PriceAmountPerItem, [PriceAmountPerItem] ) ,
									[PriceCurrencyID] = IIF( @PriceCurrencyID IS NOT NULL, @PriceCurrencyID, [PriceCurrencyID] ) ,
									[Comments] = IIF( @Comments IS NOT NULL, @Comments, [Comments] ) ,
									[PrintingHouseID] = IIF( @PrintingHouseID IS NOT NULL, @PrintingHouseID, [PrintingHouseID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderItem was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OrderItem] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN e.[Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN e.[Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SizeID IS NOT NULL THEN (CASE WHEN e.[SizeID] = @SizeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameTypeID IS NOT NULL THEN (CASE WHEN e.[FrameTypeID] = @FrameTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameSizeID IS NOT NULL THEN (CASE WHEN e.[FrameSizeID] = @FrameSizeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MatID IS NOT NULL THEN (CASE WHEN e.[MatID] = @MatID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaterialTypeID IS NOT NULL THEN (CASE WHEN e.[MaterialTypeID] = @MaterialTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MountingTypeID IS NOT NULL THEN (CASE WHEN e.[MountingTypeID] = @MountingTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ItemCount IS NOT NULL THEN (CASE WHEN e.[ItemCount] = @ItemCount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceAmountPerItem IS NOT NULL THEN (CASE WHEN e.[PriceAmountPerItem] = @PriceAmountPerItem THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN e.[PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN e.[Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_Delete]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderPaymentDetails]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[OrderPaymentDetails]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_Erase]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderPaymentDetails]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[OrderPaymentDetails] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderPaymentDetails] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderPaymentDetails] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderPaymentDetails] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderPaymentDetails] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderPaymentDetails] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetByOrderID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetByOrderID]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetByOrderID]

	@OrderID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderPaymentDetails] c 
				WHERE
					[OrderID] = @OrderID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderPaymentDetails] e
		WHERE 
			[OrderID] = @OrderID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetByPaymentMethodID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetByPaymentMethodID]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetByPaymentMethodID]

	@PaymentMethodID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderPaymentDetails] c 
				WHERE
					[PaymentMethodID] = @PaymentMethodID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderPaymentDetails] e
		WHERE 
			[PaymentMethodID] = @PaymentMethodID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderPaymentDetails] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderPaymentDetails] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_Insert]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_Insert]
			@ID BIGINT,
			@OrderID BIGINT,
			@PaymentMethodID BIGINT,
			@PaymentTransUID NVARCHAR(250),
			@PaymentDateTime DATETIME,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderPaymentDetails]
	SELECT 
		@OrderID,
		@PaymentMethodID,
		@PaymentTransUID,
		@PaymentDateTime,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderPaymentDetails] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentMethodID IS NOT NULL THEN (CASE WHEN e.[PaymentMethodID] = @PaymentMethodID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentTransUID IS NOT NULL THEN (CASE WHEN e.[PaymentTransUID] = @PaymentTransUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentDateTime IS NOT NULL THEN (CASE WHEN e.[PaymentDateTime] = @PaymentDateTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_Update]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_Update]
			@ID BIGINT,
			@OrderID BIGINT,
			@PaymentMethodID BIGINT,
			@PaymentTransUID NVARCHAR(250),
			@PaymentDateTime DATETIME,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderPaymentDetails]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OrderPaymentDetails]
		SET
									[OrderID] = IIF( @OrderID IS NOT NULL, @OrderID, [OrderID] ) ,
									[PaymentMethodID] = IIF( @PaymentMethodID IS NOT NULL, @PaymentMethodID, [PaymentMethodID] ) ,
									[PaymentTransUID] = IIF( @PaymentTransUID IS NOT NULL, @PaymentTransUID, [PaymentTransUID] ) ,
									[PaymentDateTime] = IIF( @PaymentDateTime IS NOT NULL, @PaymentDateTime, [PaymentDateTime] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderPaymentDetails was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OrderPaymentDetails] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentMethodID IS NOT NULL THEN (CASE WHEN e.[PaymentMethodID] = @PaymentMethodID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentTransUID IS NOT NULL THEN (CASE WHEN e.[PaymentTransUID] = @PaymentTransUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentDateTime IS NOT NULL THEN (CASE WHEN e.[PaymentDateTime] = @PaymentDateTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatus_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatus_Delete]
GO

CREATE PROCEDURE [dbo].[p_OrderStatus_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[OrderStatus]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatus_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatus_Erase]
GO

CREATE PROCEDURE [dbo].[p_OrderStatus_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[OrderStatus] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatus_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatus_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderStatus] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatus_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatus_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_OrderStatus_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatus] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderStatus] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatus_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatus_Insert]
GO

CREATE PROCEDURE [dbo].[p_OrderStatus_Insert]
			@ID BIGINT,
			@OrderStatusName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderStatus]
	SELECT 
		@OrderStatusName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN e.[OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatus_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatus_Update]
GO

CREATE PROCEDURE [dbo].[p_OrderStatus_Update]
			@ID BIGINT,
			@OrderStatusName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatus]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OrderStatus]
		SET
									[OrderStatusName] = IIF( @OrderStatusName IS NOT NULL, @OrderStatusName, [OrderStatusName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderStatus was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OrderStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN e.[OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_Delete]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_Delete]
		@FromStatusID BIGINT,	
		@ToStatusID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderStatusFlow]  
				WHERE 
							[FromStatusID] = @FromStatusID	AND
							[ToStatusID] = @ToStatusID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[OrderStatusFlow] 
			WHERE 
						[FromStatusID] = @FromStatusID	AND
						[ToStatusID] = @ToStatusID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderStatusFlow] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_GetByFromStatusID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_GetByFromStatusID]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_GetByFromStatusID]

	@FromStatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatusFlow] c 
				WHERE
					[FromStatusID] = @FromStatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderStatusFlow] e
		WHERE 
			[FromStatusID] = @FromStatusID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_GetByToStatusID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_GetByToStatusID]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_GetByToStatusID]

	@ToStatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatusFlow] c 
				WHERE
					[ToStatusID] = @ToStatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderStatusFlow] e
		WHERE 
			[ToStatusID] = @ToStatusID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_GetDetails]
		@FromStatusID BIGINT,	
		@ToStatusID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatusFlow] c 
				WHERE 
								[FromStatusID] = @FromStatusID	AND
								[ToStatusID] = @ToStatusID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderStatusFlow] e
		WHERE 
								[FromStatusID] = @FromStatusID	AND
								[ToStatusID] = @ToStatusID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_Insert]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_Insert]
			@FromStatusID BIGINT,
			@ToStatusID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderStatusFlow]
	SELECT 
		@FromStatusID,
		@ToStatusID
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderStatusFlow] e
	WHERE
				(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN e.[FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN e.[ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_Update]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_Update]
			@FromStatusID BIGINT,
			@ToStatusID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatusFlow]
					WHERE 
												[FromStatusID] = @FromStatusID	AND
												[ToStatusID] = @ToStatusID	
							))
	BEGIN
		UPDATE [dbo].[OrderStatusFlow]
		SET
									[FromStatusID] = IIF( @FromStatusID IS NOT NULL, @FromStatusID, [FromStatusID] ) ,
									[ToStatusID] = IIF( @ToStatusID IS NOT NULL, @ToStatusID, [ToStatusID] ) 
						WHERE 
												[FromStatusID] = @FromStatusID	AND
												[ToStatusID] = @ToStatusID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderStatusFlow was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OrderStatusFlow] e
	WHERE
				(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN e.[FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN e.[ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_Delete]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderTracking]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[OrderTracking] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderTracking] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_GetByOrderID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_GetByOrderID]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_GetByOrderID]

	@OrderID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderTracking] c 
				WHERE
					[OrderID] = @OrderID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderTracking] e
		WHERE 
			[OrderID] = @OrderID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_GetByOrderStatusID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_GetByOrderStatusID]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_GetByOrderStatusID]

	@OrderStatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderTracking] c 
				WHERE
					[OrderStatusID] = @OrderStatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderTracking] e
		WHERE 
			[OrderStatusID] = @OrderStatusID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_GetBySetByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_GetBySetByID]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_GetBySetByID]

	@SetByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderTracking] c 
				WHERE
					[SetByID] = @SetByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderTracking] e
		WHERE 
			[SetByID] = @SetByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderTracking] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderTracking] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_Insert]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_Insert]
			@ID BIGINT,
			@OrderID BIGINT,
			@OrderStatusID BIGINT,
			@SetDate DATETIME,
			@SetByID BIGINT,
			@Comment NVARCHAR(1000)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderTracking]
	SELECT 
		@OrderID,
		@OrderStatusID,
		@SetDate,
		@SetByID,
		@Comment
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderTracking] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderStatusID IS NOT NULL THEN (CASE WHEN e.[OrderStatusID] = @OrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SetDate IS NOT NULL THEN (CASE WHEN e.[SetDate] = @SetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SetByID IS NOT NULL THEN (CASE WHEN e.[SetByID] = @SetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_Update]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_Update]
			@ID BIGINT,
			@OrderID BIGINT,
			@OrderStatusID BIGINT,
			@SetDate DATETIME,
			@SetByID BIGINT,
			@Comment NVARCHAR(1000)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderTracking]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OrderTracking]
		SET
									[OrderID] = IIF( @OrderID IS NOT NULL, @OrderID, [OrderID] ) ,
									[OrderStatusID] = IIF( @OrderStatusID IS NOT NULL, @OrderStatusID, [OrderStatusID] ) ,
									[SetDate] = IIF( @SetDate IS NOT NULL, @SetDate, [SetDate] ) ,
									[SetByID] = IIF( @SetByID IS NOT NULL, @SetByID, [SetByID] ) ,
									[Comment] = IIF( @Comment IS NOT NULL, @Comment, [Comment] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderTracking was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OrderTracking] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderStatusID IS NOT NULL THEN (CASE WHEN e.[OrderStatusID] = @OrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SetDate IS NOT NULL THEN (CASE WHEN e.[SetDate] = @SetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SetByID IS NOT NULL THEN (CASE WHEN e.[SetByID] = @SetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PaymentMethod_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_PaymentMethod_Delete]
GO

CREATE PROCEDURE [dbo].[p_PaymentMethod_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PaymentMethod]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[PaymentMethod]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PaymentMethod_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_PaymentMethod_Erase]
GO

CREATE PROCEDURE [dbo].[p_PaymentMethod_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PaymentMethod]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[PaymentMethod] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PaymentMethod_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_PaymentMethod_GetAll]
GO

CREATE PROCEDURE [dbo].[p_PaymentMethod_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PaymentMethod] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PaymentMethod_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_PaymentMethod_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_PaymentMethod_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PaymentMethod] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PaymentMethod] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PaymentMethod_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_PaymentMethod_Insert]
GO

CREATE PROCEDURE [dbo].[p_PaymentMethod_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PaymentMethod]
	SELECT 
		@Name,
		@Description,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[PaymentMethod] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PaymentMethod_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_PaymentMethod_Update]
GO

CREATE PROCEDURE [dbo].[p_PaymentMethod_Update]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PaymentMethod]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[PaymentMethod]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PaymentMethod was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PaymentMethod] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_Delete]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PrintingHouse]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[PrintingHouse]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_Erase]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PrintingHouse]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[PrintingHouse] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_GetAll]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouse] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouse] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouse] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouse] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouse] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouse] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouse] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_Insert]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PrintingHouse]
	SELECT 
		@Name,
		@Description,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouse] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_Update]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_Update]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouse]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[PrintingHouse]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PrintingHouse was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouse] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_Delete]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_Delete]
		@PrintingHouseID BIGINT,	
		@AddressID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PrintingHouseAddress]  
				WHERE 
							[PrintingHouseID] = @PrintingHouseID	AND
							[AddressID] = @AddressID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[PrintingHouseAddress] 
			WHERE 
						[PrintingHouseID] = @PrintingHouseID	AND
						[AddressID] = @AddressID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_GetAll]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseAddress] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_GetByAddressID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_GetByAddressID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_GetByAddressID]

	@AddressID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseAddress] c 
				WHERE
					[AddressID] = @AddressID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseAddress] e
		WHERE 
			[AddressID] = @AddressID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_GetByPrintingHouseID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_GetByPrintingHouseID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_GetByPrintingHouseID]

	@PrintingHouseID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseAddress] c 
				WHERE
					[PrintingHouseID] = @PrintingHouseID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseAddress] e
		WHERE 
			[PrintingHouseID] = @PrintingHouseID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_GetDetails]
		@PrintingHouseID BIGINT,	
		@AddressID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseAddress] c 
				WHERE 
								[PrintingHouseID] = @PrintingHouseID	AND
								[AddressID] = @AddressID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseAddress] e
		WHERE 
								[PrintingHouseID] = @PrintingHouseID	AND
								[AddressID] = @AddressID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_Insert]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_Insert]
			@PrintingHouseID BIGINT,
			@AddressID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PrintingHouseAddress]
	SELECT 
		@PrintingHouseID,
		@AddressID,
		@IsPrimary
	
	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseAddress] e
	WHERE
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN e.[AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_Update]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_Update]
			@PrintingHouseID BIGINT,
			@AddressID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseAddress]
					WHERE 
												[PrintingHouseID] = @PrintingHouseID	AND
												[AddressID] = @AddressID	
							))
	BEGIN
		UPDATE [dbo].[PrintingHouseAddress]
		SET
									[PrintingHouseID] = IIF( @PrintingHouseID IS NOT NULL, @PrintingHouseID, [PrintingHouseID] ) ,
									[AddressID] = IIF( @AddressID IS NOT NULL, @AddressID, [AddressID] ) ,
									[IsPrimary] = IIF( @IsPrimary IS NOT NULL, @IsPrimary, [IsPrimary] ) 
						WHERE 
												[PrintingHouseID] = @PrintingHouseID	AND
												[AddressID] = @AddressID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PrintingHouseAddress was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseAddress] e
	WHERE
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN e.[AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_Delete]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_Delete]
		@PrintingHouseID BIGINT,	
		@ContactID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PrintingHouseContact]  
				WHERE 
							[PrintingHouseID] = @PrintingHouseID	AND
							[ContactID] = @ContactID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[PrintingHouseContact] 
			WHERE 
						[PrintingHouseID] = @PrintingHouseID	AND
						[ContactID] = @ContactID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_GetAll]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseContact] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_GetByContactID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_GetByContactID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetByContactID]

	@ContactID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseContact] c 
				WHERE
					[ContactID] = @ContactID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseContact] e
		WHERE 
			[ContactID] = @ContactID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_GetByPrintingHouseID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_GetByPrintingHouseID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetByPrintingHouseID]

	@PrintingHouseID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseContact] c 
				WHERE
					[PrintingHouseID] = @PrintingHouseID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseContact] e
		WHERE 
			[PrintingHouseID] = @PrintingHouseID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetDetails]
		@PrintingHouseID BIGINT,	
		@ContactID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseContact] c 
				WHERE 
								[PrintingHouseID] = @PrintingHouseID	AND
								[ContactID] = @ContactID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseContact] e
		WHERE 
								[PrintingHouseID] = @PrintingHouseID	AND
								[ContactID] = @ContactID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_Insert]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_Insert]
			@PrintingHouseID BIGINT,
			@ContactID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PrintingHouseContact]
	SELECT 
		@PrintingHouseID,
		@ContactID,
		@IsPrimary
	
	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseContact] e
	WHERE
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_Update]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_Update]
			@PrintingHouseID BIGINT,
			@ContactID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseContact]
					WHERE 
												[PrintingHouseID] = @PrintingHouseID	AND
												[ContactID] = @ContactID	
							))
	BEGIN
		UPDATE [dbo].[PrintingHouseContact]
		SET
									[PrintingHouseID] = IIF( @PrintingHouseID IS NOT NULL, @PrintingHouseID, [PrintingHouseID] ) ,
									[ContactID] = IIF( @ContactID IS NOT NULL, @ContactID, [ContactID] ) ,
									[IsPrimary] = IIF( @IsPrimary IS NOT NULL, @IsPrimary, [IsPrimary] ) 
						WHERE 
												[PrintingHouseID] = @PrintingHouseID	AND
												[ContactID] = @ContactID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PrintingHouseContact was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseContact] e
	WHERE
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_Delete]
GO

CREATE PROCEDURE [dbo].[p_Region_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Region]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Region]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_Erase]
GO

CREATE PROCEDURE [dbo].[p_Region_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Region]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Region] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Region_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Region] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_GetByCountryID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_GetByCountryID]
GO

CREATE PROCEDURE [dbo].[p_Region_GetByCountryID]

	@CountryID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Region] c 
				WHERE
					[CountryID] = @CountryID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Region] e
		WHERE 
			[CountryID] = @CountryID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Region_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Region] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Region] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_Insert]
GO

CREATE PROCEDURE [dbo].[p_Region_Insert]
			@ID BIGINT,
			@RegionName NVARCHAR(50),
			@CountryID BIGINT,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Region]
	SELECT 
		@RegionName,
		@CountryID,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[Region] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RegionName IS NOT NULL THEN (CASE WHEN e.[RegionName] = @RegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CountryID IS NOT NULL THEN (CASE WHEN e.[CountryID] = @CountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_Update]
GO

CREATE PROCEDURE [dbo].[p_Region_Update]
			@ID BIGINT,
			@RegionName NVARCHAR(50),
			@CountryID BIGINT,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Region]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Region]
		SET
									[RegionName] = IIF( @RegionName IS NOT NULL, @RegionName, [RegionName] ) ,
									[CountryID] = IIF( @CountryID IS NOT NULL, @CountryID, [CountryID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Region was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Region] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RegionName IS NOT NULL THEN (CASE WHEN e.[RegionName] = @RegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CountryID IS NOT NULL THEN (CASE WHEN e.[CountryID] = @CountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_Delete]
GO

CREATE PROCEDURE [dbo].[p_Size_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Size]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Size]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_Erase]
GO

CREATE PROCEDURE [dbo].[p_Size_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Size]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Size] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Size_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Size] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_Size_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Size] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Size] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_Size_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Size] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Size] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Size_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Size] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Size] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_Insert]
GO

CREATE PROCEDURE [dbo].[p_Size_Insert]
			@ID BIGINT,
			@SizeName NVARCHAR(50),
			@Width INT,
			@Height INT,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Size]
	SELECT 
		@SizeName,
		@Width,
		@Height,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Size] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SizeName IS NOT NULL THEN (CASE WHEN e.[SizeName] = @SizeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN e.[Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN e.[Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_Update]
GO

CREATE PROCEDURE [dbo].[p_Size_Update]
			@ID BIGINT,
			@SizeName NVARCHAR(50),
			@Width INT,
			@Height INT,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Size]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Size]
		SET
									[SizeName] = IIF( @SizeName IS NOT NULL, @SizeName, [SizeName] ) ,
									[Width] = IIF( @Width IS NOT NULL, @Width, [Width] ) ,
									[Height] = IIF( @Height IS NOT NULL, @Height, [Height] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Size was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Size] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SizeName IS NOT NULL THEN (CASE WHEN e.[SizeName] = @SizeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN e.[Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN e.[Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_Delete]
GO

CREATE PROCEDURE [dbo].[p_Unit_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Unit]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Unit]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_Erase]
GO

CREATE PROCEDURE [dbo].[p_Unit_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Unit]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[Unit] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Unit_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Unit] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Unit_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Unit] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Unit] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_Insert]
GO

CREATE PROCEDURE [dbo].[p_Unit_Insert]
			@ID BIGINT,
			@UnitName NVARCHAR(50),
			@UnitAbbr NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Unit]
	SELECT 
		@UnitName,
		@UnitAbbr,
		@Description,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[Unit] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnitName IS NOT NULL THEN (CASE WHEN e.[UnitName] = @UnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnitAbbr IS NOT NULL THEN (CASE WHEN e.[UnitAbbr] = @UnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_Update]
GO

CREATE PROCEDURE [dbo].[p_Unit_Update]
			@ID BIGINT,
			@UnitName NVARCHAR(50),
			@UnitAbbr NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Unit]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Unit]
		SET
									[UnitName] = IIF( @UnitName IS NOT NULL, @UnitName, [UnitName] ) ,
									[UnitAbbr] = IIF( @UnitAbbr IS NOT NULL, @UnitAbbr, [UnitAbbr] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Unit was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Unit] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnitName IS NOT NULL THEN (CASE WHEN e.[UnitName] = @UnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnitAbbr IS NOT NULL THEN (CASE WHEN e.[UnitAbbr] = @UnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_Delete]
GO

CREATE PROCEDURE [dbo].[p_User_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[User]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[User] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetAll]
GO

CREATE PROCEDURE [dbo].[p_User_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[User] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_User_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[User] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[User] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetByUserStatusID', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetByUserStatusID]
GO

CREATE PROCEDURE [dbo].[p_User_GetByUserStatusID]

	@UserStatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[User] c 
				WHERE
					[UserStatusID] = @UserStatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[User] e
		WHERE 
			[UserStatusID] = @UserStatusID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetByUserTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetByUserTypeID]
GO

CREATE PROCEDURE [dbo].[p_User_GetByUserTypeID]

	@UserTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[User] c 
				WHERE
					[UserTypeID] = @UserTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[User] e
		WHERE 
			[UserTypeID] = @UserTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_User_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[User] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[User] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_Insert]
GO

CREATE PROCEDURE [dbo].[p_User_Insert]
			@ID BIGINT,
			@Login NVARCHAR(250),
			@PwdHash NVARCHAR(250),
			@Salt NVARCHAR(50),
			@FirstName NVARCHAR(50),
			@MiddleName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@FriendlyName NVARCHAR(50),
			@UserStatusID BIGINT,
			@UserTypeID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[User]
	SELECT 
		@Login,
		@PwdHash,
		@Salt,
		@FirstName,
		@MiddleName,
		@LastName,
		@FriendlyName,
		@UserStatusID,
		@UserTypeID,
		@CreatedDate,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[User] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN e.[Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN e.[PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN e.[Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN e.[MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN e.[FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserStatusID IS NOT NULL THEN (CASE WHEN e.[UserStatusID] = @UserStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserTypeID IS NOT NULL THEN (CASE WHEN e.[UserTypeID] = @UserTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_Update]
GO

CREATE PROCEDURE [dbo].[p_User_Update]
			@ID BIGINT,
			@Login NVARCHAR(250),
			@PwdHash NVARCHAR(250),
			@Salt NVARCHAR(50),
			@FirstName NVARCHAR(50),
			@MiddleName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@FriendlyName NVARCHAR(50),
			@UserStatusID BIGINT,
			@UserTypeID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[User]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[User]
		SET
									[Login] = IIF( @Login IS NOT NULL, @Login, [Login] ) ,
									[PwdHash] = IIF( @PwdHash IS NOT NULL, @PwdHash, [PwdHash] ) ,
									[Salt] = IIF( @Salt IS NOT NULL, @Salt, [Salt] ) ,
									[FirstName] = IIF( @FirstName IS NOT NULL, @FirstName, [FirstName] ) ,
									[MiddleName] = IIF( @MiddleName IS NOT NULL, @MiddleName, [MiddleName] ) ,
									[LastName] = IIF( @LastName IS NOT NULL, @LastName, [LastName] ) ,
									[FriendlyName] = IIF( @FriendlyName IS NOT NULL, @FriendlyName, [FriendlyName] ) ,
									[UserStatusID] = IIF( @UserStatusID IS NOT NULL, @UserStatusID, [UserStatusID] ) ,
									[UserTypeID] = IIF( @UserTypeID IS NOT NULL, @UserTypeID, [UserTypeID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'User was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[User] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN e.[Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN e.[PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN e.[Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN e.[MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN e.[FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserStatusID IS NOT NULL THEN (CASE WHEN e.[UserStatusID] = @UserStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserTypeID IS NOT NULL THEN (CASE WHEN e.[UserTypeID] = @UserTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_Delete]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_Delete]
		@UserID BIGINT,	
		@AddressID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserAddress]  
				WHERE 
							[UserID] = @UserID	AND
							[AddressID] = @AddressID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[UserAddress] 
			WHERE 
						[UserID] = @UserID	AND
						[AddressID] = @AddressID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserAddress] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_GetByAddressID', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_GetByAddressID]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_GetByAddressID]

	@AddressID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserAddress] c 
				WHERE
					[AddressID] = @AddressID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserAddress] e
		WHERE 
			[AddressID] = @AddressID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_GetByUserID', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_GetByUserID]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserAddress] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserAddress] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_GetDetails]
		@UserID BIGINT,	
		@AddressID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserAddress] c 
				WHERE 
								[UserID] = @UserID	AND
								[AddressID] = @AddressID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserAddress] e
		WHERE 
								[UserID] = @UserID	AND
								[AddressID] = @AddressID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_Insert]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_Insert]
			@UserID BIGINT,
			@AddressID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserAddress]
	SELECT 
		@UserID,
		@AddressID,
		@IsPrimary
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserAddress] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN e.[AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_Update]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_Update]
			@UserID BIGINT,
			@AddressID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserAddress]
					WHERE 
												[UserID] = @UserID	AND
												[AddressID] = @AddressID	
							))
	BEGIN
		UPDATE [dbo].[UserAddress]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[AddressID] = IIF( @AddressID IS NOT NULL, @AddressID, [AddressID] ) ,
									[IsPrimary] = IIF( @IsPrimary IS NOT NULL, @IsPrimary, [IsPrimary] ) 
						WHERE 
												[UserID] = @UserID	AND
												[AddressID] = @AddressID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserAddress was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserAddress] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN e.[AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserConfirmation_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserConfirmation_Delete]
GO

CREATE PROCEDURE [dbo].[p_UserConfirmation_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserConfirmation]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[UserConfirmation] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserConfirmation_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserConfirmation_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserConfirmation_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserConfirmation] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserConfirmation_GetByUserID', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserConfirmation_GetByUserID]
GO

CREATE PROCEDURE [dbo].[p_UserConfirmation_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserConfirmation] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserConfirmation] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserConfirmation_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserConfirmation_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_UserConfirmation_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserConfirmation] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserConfirmation] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserConfirmation_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserConfirmation_Insert]
GO

CREATE PROCEDURE [dbo].[p_UserConfirmation_Insert]
			@ID BIGINT,
			@UserID BIGINT,
			@ConfirmationCode NVARCHAR(50),
			@Comfirmed BIT,
			@ExpiresDate DATETIME,
			@ConfirmationDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserConfirmation]
	SELECT 
		@UserID,
		@ConfirmationCode,
		@Comfirmed,
		@ExpiresDate,
		@ConfirmationDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserConfirmation] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN e.[ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN e.[Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN e.[ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN e.[ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserConfirmation_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserConfirmation_Update]
GO

CREATE PROCEDURE [dbo].[p_UserConfirmation_Update]
			@ID BIGINT,
			@UserID BIGINT,
			@ConfirmationCode NVARCHAR(50),
			@Comfirmed BIT,
			@ExpiresDate DATETIME,
			@ConfirmationDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserConfirmation]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[UserConfirmation]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[ConfirmationCode] = IIF( @ConfirmationCode IS NOT NULL, @ConfirmationCode, [ConfirmationCode] ) ,
									[Comfirmed] = IIF( @Comfirmed IS NOT NULL, @Comfirmed, [Comfirmed] ) ,
									[ExpiresDate] = IIF( @ExpiresDate IS NOT NULL, @ExpiresDate, [ExpiresDate] ) ,
									[ConfirmationDate] = IIF( @ConfirmationDate IS NOT NULL, @ConfirmationDate, [ConfirmationDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserConfirmation was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserConfirmation] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN e.[ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN e.[Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN e.[ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN e.[ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_Delete]
GO

CREATE PROCEDURE [dbo].[p_UserContact_Delete]
		@UserID BIGINT,	
		@ContactID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserContact]  
				WHERE 
							[UserID] = @UserID	AND
							[ContactID] = @ContactID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[UserContact] 
			WHERE 
						[UserID] = @UserID	AND
						[ContactID] = @ContactID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserContact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserContact] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_GetByContactID', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_GetByContactID]
GO

CREATE PROCEDURE [dbo].[p_UserContact_GetByContactID]

	@ContactID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserContact] c 
				WHERE
					[ContactID] = @ContactID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserContact] e
		WHERE 
			[ContactID] = @ContactID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_GetByUserID', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_GetByUserID]
GO

CREATE PROCEDURE [dbo].[p_UserContact_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserContact] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserContact] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_UserContact_GetDetails]
		@UserID BIGINT,	
		@ContactID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserContact] c 
				WHERE 
								[UserID] = @UserID	AND
								[ContactID] = @ContactID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserContact] e
		WHERE 
								[UserID] = @UserID	AND
								[ContactID] = @ContactID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_Insert]
GO

CREATE PROCEDURE [dbo].[p_UserContact_Insert]
			@UserID BIGINT,
			@ContactID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserContact]
	SELECT 
		@UserID,
		@ContactID,
		@IsPrimary
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserContact] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_Update]
GO

CREATE PROCEDURE [dbo].[p_UserContact_Update]
			@UserID BIGINT,
			@ContactID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserContact]
					WHERE 
												[UserID] = @UserID	AND
												[ContactID] = @ContactID	
							))
	BEGIN
		UPDATE [dbo].[UserContact]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[ContactID] = IIF( @ContactID IS NOT NULL, @ContactID, [ContactID] ) ,
									[IsPrimary] = IIF( @IsPrimary IS NOT NULL, @IsPrimary, [IsPrimary] ) 
						WHERE 
												[UserID] = @UserID	AND
												[ContactID] = @ContactID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserContact was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserContact] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserStatus_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserStatus_Delete]
GO

CREATE PROCEDURE [dbo].[p_UserStatus_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[UserStatus]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserStatus_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserStatus_Erase]
GO

CREATE PROCEDURE [dbo].[p_UserStatus_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[UserStatus] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserStatus_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserStatus_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserStatus] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserStatus_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserStatus_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_UserStatus_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserStatus] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserStatus] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserStatus_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserStatus_Insert]
GO

CREATE PROCEDURE [dbo].[p_UserStatus_Insert]
			@ID BIGINT,
			@StatusName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserStatus]
	SELECT 
		@StatusName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN e.[StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserStatus_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserStatus_Update]
GO

CREATE PROCEDURE [dbo].[p_UserStatus_Update]
			@ID BIGINT,
			@StatusName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserStatus]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[UserStatus]
		SET
									[StatusName] = IIF( @StatusName IS NOT NULL, @StatusName, [StatusName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserStatus was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN e.[StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserType_Delete]
GO

CREATE PROCEDURE [dbo].[p_UserType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[UserType]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserType_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserType_Erase]
GO

CREATE PROCEDURE [dbo].[p_UserType_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[UserType] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserType] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_UserType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserType_Insert]
GO

CREATE PROCEDURE [dbo].[p_UserType_Insert]
			@ID BIGINT,
			@UserTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserType]
	SELECT 
		@UserTypeName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN e.[UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserType_Update]
GO

CREATE PROCEDURE [dbo].[p_UserType_Update]
			@ID BIGINT,
			@UserTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[UserType]
		SET
									[UserTypeName] = IIF( @UserTypeName IS NOT NULL, @UserTypeName, [UserTypeName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN e.[UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO

/******************** Populating Reference Data **************************/
PRINT 'Populating reference data'
EXEC dbo.p_AddressType_Populate

EXEC dbo.p_Country_Populate

EXEC dbo.p_Region_Populate

EXEC dbo.p_City_Populate

EXEC dbo.p_ContactType_Populate

EXEC dbo.p_Currency_Populate

EXEC dbo.p_OrderStatus_Populate

EXEC dbo.p_PaymentMethod_Populate

EXEC dbo.p_Unit_Populate

EXEC dbo.p_UserStatus_Populate

EXEC dbo.p_UserType_Populate

PRINT 'Populating test data'
IF(NOT EXISTS(SELECT COUNT(1) FROM [dbo].[User] ))
BEGIN
	EXEC p_TestData_Populate '/sql/testdata/'
END
ELSE
BEGIN
	PRINT 'Test data present - skipping'
END

PRINT('Done')


