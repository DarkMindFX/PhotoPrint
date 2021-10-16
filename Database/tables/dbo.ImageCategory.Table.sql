USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[ImageCategory]    Script Date: 10/16/2021 9:42:42 PM ******/
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
