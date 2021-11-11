CREATE TABLE [dbo].[User] (
    [ID]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Login]        NVARCHAR (250) NOT NULL,
    [PwdHash]      NVARCHAR (250) NOT NULL,
    [Salt]         NVARCHAR (50)  NOT NULL,
    [FirstName]    NVARCHAR (50)  NOT NULL,
    [MiddleName]   NVARCHAR (50)  NULL,
    [LastName]     NVARCHAR (50)  NOT NULL,
    [FriendlyName] NVARCHAR (50)  NULL,
    [UserStatusID] BIGINT         NOT NULL,
    [UserTypeID]   BIGINT         NOT NULL,
    [CreatedDate]  DATETIME       NOT NULL,
    [ModifiedDate] DATETIME       NULL,
    [ModifiedByID] BIGINT         NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_User_User] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_User_UserStatus] FOREIGN KEY ([UserStatusID]) REFERENCES [dbo].[UserStatus] ([ID]),
    CONSTRAINT [FK_User_UserType] FOREIGN KEY ([UserTypeID]) REFERENCES [dbo].[UserType] ([ID])
);

