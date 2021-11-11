CREATE TABLE [dbo].[UserContact] (
    [UserID]    BIGINT NOT NULL,
    [ContactID] BIGINT NOT NULL,
    [IsPrimary] BIT    CONSTRAINT [DF_UserContact_IsPrimary] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserContact] PRIMARY KEY CLUSTERED ([UserID] ASC, [ContactID] ASC),
    CONSTRAINT [FK_UserContact_Contact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[Contact] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserContact_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE
);

