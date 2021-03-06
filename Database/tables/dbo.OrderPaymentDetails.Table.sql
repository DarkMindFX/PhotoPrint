USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[OrderPaymentDetails]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderPaymentDetails](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[PaymentMethodID] [bigint] NOT NULL,
	[PaymentTransUID] [nvarchar](250) NULL,
	[PaymentDateTime] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_OrderPaymentDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderPaymentDetails] ADD  CONSTRAINT [DF_OrderPaymentDetails_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[OrderPaymentDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderPaymentDetails_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[OrderPaymentDetails] CHECK CONSTRAINT [FK_OrderPaymentDetails_CreatedByUser]
GO
ALTER TABLE [dbo].[OrderPaymentDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderPaymentDetails_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[OrderPaymentDetails] CHECK CONSTRAINT [FK_OrderPaymentDetails_ModifiedByUser]
GO
ALTER TABLE [dbo].[OrderPaymentDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderPaymentDetails_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderPaymentDetails] CHECK CONSTRAINT [FK_OrderPaymentDetails_Order]
GO
ALTER TABLE [dbo].[OrderPaymentDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderPaymentDetails_PaymentMethod] FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[PaymentMethod] ([ID])
GO
ALTER TABLE [dbo].[OrderPaymentDetails] CHECK CONSTRAINT [FK_OrderPaymentDetails_PaymentMethod]
GO
