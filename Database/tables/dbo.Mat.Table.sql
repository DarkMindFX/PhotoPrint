USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Mat]    Script Date: 2/18/2022 11:15:37 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
