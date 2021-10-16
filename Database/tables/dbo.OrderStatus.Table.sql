USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 10/16/2021 9:42:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderStatusName] [nvarchar](50) NOT NULL,
	[IsDeleted] [bigint] NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderStatus] ADD  CONSTRAINT [DF_OrderStatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
