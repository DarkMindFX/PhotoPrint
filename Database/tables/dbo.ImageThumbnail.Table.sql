USE [PhotoPrint]
GO
ALTER TABLE [dbo].[ImageThumbnail] DROP CONSTRAINT [FK_ImageThumbnail_Image]
GO
/****** Object:  Table [dbo].[ImageThumbnail]    Script Date: 10/28/2021 9:10:49 PM ******/
DROP TABLE [dbo].[ImageThumbnail]
GO
/****** Object:  Table [dbo].[ImageThumbnail]    Script Date: 10/28/2021 9:10:49 PM ******/
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
