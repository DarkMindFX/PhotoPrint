USE [PhotoPrint]
GO
ALTER TABLE [dbo].[PrintingHouse] DROP CONSTRAINT [FK_PrintingHouse_ModifiedByUser]
GO
ALTER TABLE [dbo].[PrintingHouse] DROP CONSTRAINT [FK_PrintingHouse_CreatedByUser]
GO
ALTER TABLE [dbo].[PrintingHouse] DROP CONSTRAINT [DF_PrintingHouse_IsEnabled]
GO
/****** Object:  Table [dbo].[PrintingHouse]    Script Date: 10/28/2021 9:10:49 PM ******/
DROP TABLE [dbo].[PrintingHouse]
GO
/****** Object:  Table [dbo].[PrintingHouse]    Script Date: 10/28/2021 9:10:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintingHouse](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_PrintingHouse] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrintingHouse] ADD  CONSTRAINT [DF_PrintingHouse_IsEnabled]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PrintingHouse]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouse_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouse] CHECK CONSTRAINT [FK_PrintingHouse_CreatedByUser]
GO
ALTER TABLE [dbo].[PrintingHouse]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouse_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouse] CHECK CONSTRAINT [FK_PrintingHouse_ModifiedByUser]
GO
