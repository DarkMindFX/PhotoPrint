USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[FrameType]    Script Date: 2/18/2022 11:15:37 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
