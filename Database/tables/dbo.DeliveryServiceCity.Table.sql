USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[DeliveryServiceCity]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryServiceCity](
	[DeliveryServiceID] [bigint] NOT NULL,
	[CityID] [bigint] NOT NULL,
 CONSTRAINT [PK_DeliveryServiceCity_1] PRIMARY KEY CLUSTERED 
(
	[DeliveryServiceID] ASC,
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeliveryServiceCity]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryServiceCity_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeliveryServiceCity] CHECK CONSTRAINT [FK_DeliveryServiceCity_City]
GO
ALTER TABLE [dbo].[DeliveryServiceCity]  WITH NOCHECK ADD  CONSTRAINT [FK_DeliveryServiceCity_DeliveryService] FOREIGN KEY([DeliveryServiceID])
REFERENCES [dbo].[DeliveryService] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeliveryServiceCity] CHECK CONSTRAINT [FK_DeliveryServiceCity_DeliveryService]
GO
