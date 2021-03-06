USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserInteriorThumbnail]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInteriorThumbnail](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_UserInteriorThumbnail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserInteriorThumbnail]  WITH CHECK ADD  CONSTRAINT [FK_UserInteriorThumbnail_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserInteriorThumbnail] CHECK CONSTRAINT [FK_UserInteriorThumbnail_User]
GO
