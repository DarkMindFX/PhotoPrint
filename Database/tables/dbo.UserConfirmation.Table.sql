USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserConfirmation]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserConfirmation](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[ConfirmationCode] [nvarchar](50) NOT NULL,
	[Comfirmed] [bit] NOT NULL,
	[ExpiresDate] [datetime] NOT NULL,
	[ConfirmationDate] [datetime] NULL,
 CONSTRAINT [PK_UserConfirmation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserConfirmation] ADD  CONSTRAINT [DF_UserConfirmation_Comfirmed]  DEFAULT ((0)) FOR [Comfirmed]
GO
ALTER TABLE [dbo].[UserConfirmation]  WITH NOCHECK ADD  CONSTRAINT [FK_UserConfirmation_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserConfirmation] CHECK CONSTRAINT [FK_UserConfirmation_User]
GO
