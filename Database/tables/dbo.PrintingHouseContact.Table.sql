USE [PhotoPrint]
GO
ALTER TABLE [dbo].[PrintingHouseContact] DROP CONSTRAINT [FK_PrintingHouseContact_PrintingHouseContact]
GO
ALTER TABLE [dbo].[PrintingHouseContact] DROP CONSTRAINT [FK_PrintingHouseContact_Contact]
GO
ALTER TABLE [dbo].[PrintingHouseContact] DROP CONSTRAINT [DF_PrintingHouseContact_IsPrimary]
GO
/****** Object:  Table [dbo].[PrintingHouseContact]    Script Date: 10/28/2021 9:10:49 PM ******/
DROP TABLE [dbo].[PrintingHouseContact]
GO
/****** Object:  Table [dbo].[PrintingHouseContact]    Script Date: 10/28/2021 9:10:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintingHouseContact](
	[PrintingHouseID] [bigint] NOT NULL,
	[ContactID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_PrintingHouseContact_1] PRIMARY KEY CLUSTERED 
(
	[PrintingHouseID] ASC,
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrintingHouseContact] ADD  CONSTRAINT [DF_PrintingHouseContact_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[PrintingHouseContact]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouseContact_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contact] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouseContact] CHECK CONSTRAINT [FK_PrintingHouseContact_Contact]
GO
ALTER TABLE [dbo].[PrintingHouseContact]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouseContact_PrintingHouseContact] FOREIGN KEY([PrintingHouseID])
REFERENCES [dbo].[PrintingHouse] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PrintingHouseContact] CHECK CONSTRAINT [FK_PrintingHouseContact_PrintingHouseContact]
GO
