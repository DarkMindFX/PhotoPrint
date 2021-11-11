CREATE TABLE [dbo].[PrintingHouseAddress] (
    [PrintingHouseID] BIGINT NOT NULL,
    [AddressID]       BIGINT NOT NULL,
    [IsPrimary]       BIT    CONSTRAINT [DF_PrintingHouseAddress_IsPrimary] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PrintingHouseAddress_1] PRIMARY KEY CLUSTERED ([PrintingHouseID] ASC, [AddressID] ASC),
    CONSTRAINT [FK_PrintingHouseAddress_Address] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([ID]),
    CONSTRAINT [FK_PrintingHouseAddress_PrintingHouse] FOREIGN KEY ([PrintingHouseID]) REFERENCES [dbo].[PrintingHouse] ([ID])
);

