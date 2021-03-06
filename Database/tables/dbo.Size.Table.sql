USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SizeName] [nvarchar](50) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Size] ADD  CONSTRAINT [DF_Size_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Size]  WITH NOCHECK ADD  CONSTRAINT [FK_Size_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Size] CHECK CONSTRAINT [FK_Size_CreatedByUser]
GO
ALTER TABLE [dbo].[Size]  WITH NOCHECK ADD  CONSTRAINT [FK_Size_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Size] CHECK CONSTRAINT [FK_Size_ModifiedByUser]
GO
