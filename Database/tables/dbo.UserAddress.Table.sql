USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserAddress]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAddress](
	[UserID] [bigint] NOT NULL,
	[AddressID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_UserAddress] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserAddress] ADD  CONSTRAINT [DF_UserAddress_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[UserAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_UserAddress_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAddress] CHECK CONSTRAINT [FK_UserAddress_Address]
GO
ALTER TABLE [dbo].[UserAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_UserAddress_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAddress] CHECK CONSTRAINT [FK_UserAddress_User]
GO
