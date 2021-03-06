USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[ImageRelated]    Script Date: 2/18/2022 11:15:37 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
