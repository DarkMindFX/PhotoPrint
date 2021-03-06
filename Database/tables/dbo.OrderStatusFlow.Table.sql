USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderStatusFlow]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatusFlow](
	[FromStatusID] [bigint] NOT NULL,
	[ToStatusID] [bigint] NOT NULL,
 CONSTRAINT [PK_OrderStatusFlow] PRIMARY KEY CLUSTERED 
(
	[FromStatusID] ASC,
	[ToStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderStatusFlow]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderStatusFlow_StatusFrom] FOREIGN KEY([FromStatusID])
REFERENCES [dbo].[OrderStatus] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderStatusFlow] CHECK CONSTRAINT [FK_OrderStatusFlow_StatusFrom]
GO
ALTER TABLE [dbo].[OrderStatusFlow]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderStatusFlow_StatusTo] FOREIGN KEY([ToStatusID])
REFERENCES [dbo].[OrderStatus] ([ID])
GO
ALTER TABLE [dbo].[OrderStatusFlow] CHECK CONSTRAINT [FK_OrderStatusFlow_StatusTo]
GO
