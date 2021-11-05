USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 11/5/2021 11:18:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserStatus] ADD  CONSTRAINT [DF_UserStatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
