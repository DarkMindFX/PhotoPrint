USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[DeliveryService]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryService](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeliveryServiceName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByID] [bigint] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByID] [bigint] NULL,
 CONSTRAINT [PK_DeliveryType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeliveryService] ADD  CONSTRAINT [DF_DeliveryType_IsEnabled]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DeliveryService]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryService_CreatedByUser] FOREIGN KEY([CreatedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[DeliveryService] CHECK CONSTRAINT [FK_DeliveryService_CreatedByUser]
GO
ALTER TABLE [dbo].[DeliveryService]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryService_ModifiedByUser] FOREIGN KEY([ModifiedByID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[DeliveryService] CHECK CONSTRAINT [FK_DeliveryService_ModifiedByUser]
GO
