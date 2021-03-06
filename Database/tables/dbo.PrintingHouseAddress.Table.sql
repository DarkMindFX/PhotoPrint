USE [PhotoPrint]
GO
/****** Object:  Table [dbo].[PrintingHouseAddress]    Script Date: 2/18/2022 11:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintingHouseAddress](
	[PrintingHouseID] [bigint] NOT NULL,
	[AddressID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_PrintingHouseAddress_1] PRIMARY KEY CLUSTERED 
(
	[PrintingHouseID] ASC,
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PrintingHouseAddress] ADD  CONSTRAINT [DF_PrintingHouseAddress_IsPrimary]  DEFAULT ((0)) FOR [IsPrimary]
GO
ALTER TABLE [dbo].[PrintingHouseAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouseAddress_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouseAddress] CHECK CONSTRAINT [FK_PrintingHouseAddress_Address]
GO
ALTER TABLE [dbo].[PrintingHouseAddress]  WITH NOCHECK ADD  CONSTRAINT [FK_PrintingHouseAddress_PrintingHouse] FOREIGN KEY([PrintingHouseID])
REFERENCES [dbo].[PrintingHouse] ([ID])
GO
ALTER TABLE [dbo].[PrintingHouseAddress] CHECK CONSTRAINT [FK_PrintingHouseAddress_PrintingHouse]
GO
