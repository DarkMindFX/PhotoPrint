USE [PhotoPrint]
GO
ALTER TABLE [dbo].[Currency] DROP CONSTRAINT [DF_Currency_IsDeleted]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 10/28/2021 9:10:49 PM ******/
DROP TABLE [dbo].[Currency]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 10/28/2021 9:10:49 PM ******/
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
