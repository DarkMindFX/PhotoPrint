USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[ImageRelated]    Script Date: 10/16/2021 9:42:42 PM ******/
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
