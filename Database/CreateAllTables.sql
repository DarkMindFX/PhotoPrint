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
