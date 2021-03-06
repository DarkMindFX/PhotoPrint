USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[ParentID] [bigint] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_ImageCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Category]  WITH NOCHECK ADD  CONSTRAINT [FK_Category_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_CreatedByUser]
GO
ALTER TABLE [dbo].[Category]  WITH NOCHECK ADD  CONSTRAINT [FK_Category_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_ModifiedByUser]
GO
ALTER TABLE [dbo].[Category]  WITH NOCHECK ADD  CONSTRAINT [FK_ImageCategory_ImageCategory] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_ImageCategory_ImageCategory]
GO
