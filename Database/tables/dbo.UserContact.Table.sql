USE [PhotoPrint]
GO
ALTER TABLE [dbo].[UserContact] DROP CONSTRAINT [FK_UserContact_User]
GO
ALTER TABLE [dbo].[UserContact] DROP CONSTRAINT [FK_UserContact_Contact]
GO
ALTER TABLE [dbo].[UserContact] DROP CONSTRAINT [DF_UserContact_IsPrimary]
GO
/****** Object:  Table [dbo].[UserContact]    Script Date: 10/28/2021 9:10:49 PM ******/
DROP TABLE [dbo].[UserContact]
GO
/****** Object:  Table [dbo].[UserContact]    Script Date: 10/28/2021 9:10:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserContact](
	[UserID] [bigint] NOT NULL,
	[ContactID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_UserContact] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserContact] ADD  CONSTRAINT [DF_UserContact_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[UserContact]  WITH NOCHECK ADD  CONSTRAINT [FK_UserContact_Contact] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contact] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserContact] CHECK CONSTRAINT [FK_UserContact_Contact]
GO
ALTER TABLE [dbo].[UserContact]  WITH NOCHECK ADD  CONSTRAINT [FK_UserContact_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserContact] CHECK CONSTRAINT [FK_UserContact_User]
GO
