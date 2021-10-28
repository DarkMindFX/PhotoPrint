USE [PhotoPrint]
GO
ALTER TABLE [dbo].[ContactType] DROP CONSTRAINT [DF_ContactType_IsDeleted]
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 10/28/2021 9:10:49 PM ******/
DROP TABLE [dbo].[ContactType]
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 10/28/2021 9:10:49 PM ******/
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
