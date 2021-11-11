CREATE TABLE [dbo].[UserConfirmation] (
    [ID]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserID]           BIGINT        NOT NULL,
    [ConfirmationCode] NVARCHAR (50) NOT NULL,
    [Comfirmed]        BIT           CONSTRAINT [DF_UserConfirmation_Comfirmed] DEFAULT ((0)) NOT NULL,
    [ExpiresDate]      DATETIME      NOT NULL,
    [ConfirmationDate] DATETIME      NULL,
    CONSTRAINT [PK_UserConfirmation] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserConfirmation_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

