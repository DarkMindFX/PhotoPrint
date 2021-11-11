CREATE TABLE [dbo].[PrintingHouseContact] (
    [PrintingHouseID] BIGINT NOT NULL,
    [ContactID]       BIGINT NOT NULL,
    [IsPrimary]       BIT    CONSTRAINT [DF_PrintingHouseContact_IsPrimary] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PrintingHouseContact_1] PRIMARY KEY CLUSTERED ([PrintingHouseID] ASC, [ContactID] ASC),
    CONSTRAINT [FK_PrintingHouseContact_Contact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[Contact] ([ID]),
    CONSTRAINT [FK_PrintingHouseContact_PrintingHouseContact] FOREIGN KEY ([PrintingHouseID]) REFERENCES [dbo].[PrintingHouse] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

