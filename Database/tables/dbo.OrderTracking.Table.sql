USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderTracking]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTracking](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[OrderStatusID] [bigint] NOT NULL,
	[SetDate] [datetime] NOT NULL,
	[SetByID] [bigint] NOT NULL,
	[Comment] [nvarchar](1000) NULL,
 CONSTRAINT [PK_OrderTracking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderTracking]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderTracking_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderTracking] CHECK CONSTRAINT [FK_OrderTracking_Order]
GO
ALTER TABLE [dbo].[OrderTracking]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderTracking_OrderStatus] FOREIGN KEY([OrderStatusID])
REFERENCES [dbo].[OrderStatus] ([ID])
GO
ALTER TABLE [dbo].[OrderTracking] CHECK CONSTRAINT [FK_OrderTracking_OrderStatus]
GO
ALTER TABLE [dbo].[OrderTracking]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderTracking_User] FOREIGN KEY([SetByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[OrderTracking] CHECK CONSTRAINT [FK_OrderTracking_User]
GO
