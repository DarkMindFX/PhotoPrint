CREATE TABLE [dbo].[UserAddress] (
    [UserID]    BIGINT NOT NULL,
    [AddressID] BIGINT NOT NULL,
    [IsPrimary] BIT    CONSTRAINT [DF_UserAddress_IsPrimary] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserAddress] PRIMARY KEY CLUSTERED ([UserID] ASC, [AddressID] ASC),
    CONSTRAINT [FK_UserAddress_Address] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserAddress_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE
);

