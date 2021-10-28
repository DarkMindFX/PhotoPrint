USE [PhotoPrint]
GO
ALTER TABLE [dbo].[UserStatus] DROP CONSTRAINT [DF_UserStatus_IsDeleted]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 10/28/2021 9:10:49 PM ******/
DROP TABLE [dbo].[UserStatus]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 10/28/2021 9:10:49 PM ******/
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
