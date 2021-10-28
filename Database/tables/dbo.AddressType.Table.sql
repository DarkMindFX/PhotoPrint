USE [PhotoPrint]
GO
ALTER TABLE [dbo].[AddressType] DROP CONSTRAINT [DF_AddressType_IsDeleted]
GO
/****** Object:  Table [dbo].[AddressType]    Script Date: 10/28/2021 9:10:49 PM ******/
DROP TABLE [dbo].[AddressType]
GO
/****** Object:  Table [dbo].[AddressType]    Script Date: 10/28/2021 9:10:49 PM ******/
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
