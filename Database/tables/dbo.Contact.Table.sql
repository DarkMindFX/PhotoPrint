USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ContactTypeID] [bigint] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Comment] [nvarchar](250) NULL,
	[Value] [nvarchar](1000) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedByID] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_IsPrimary]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Contact]  WITH NOCHECK ADD  CONSTRAINT [FK_Contact_ContactType] FOREIGN KEY([ContactTypeID])
REFERENCES [dbo].[ContactType] ([ID])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_ContactType]
GO
ALTER TABLE [dbo].[Contact]  WITH NOCHECK ADD  CONSTRAINT [FK_Contact_UserCreated] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_UserCreated]
GO
ALTER TABLE [dbo].[Contact]  WITH NOCHECK ADD  CONSTRAINT [FK_Contact_UserModified] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_UserModified]
GO
