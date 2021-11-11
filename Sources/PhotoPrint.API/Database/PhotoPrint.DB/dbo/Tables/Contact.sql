CREATE TABLE [dbo].[Contact] (
    [ID]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [ContactTypeID] BIGINT          NOT NULL,
    [Title]         NVARCHAR (50)   NOT NULL,
    [Comment]       NVARCHAR (250)  NULL,
    [Value]         NVARCHAR (1000) NOT NULL,
    [IsDeleted]     BIT             CONSTRAINT [DF_Contact_IsPrimary] DEFAULT ((0)) NOT NULL,
    [CreatedByID]   BIGINT          NOT NULL,
    [CreatedDate]   DATETIME        NOT NULL,
    [ModifiedByID]  BIGINT          NULL,
    [ModifiedDate]  DATETIME        NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Contact_ContactType] FOREIGN KEY ([ContactTypeID]) REFERENCES [dbo].[ContactType] ([ID]),
    CONSTRAINT [FK_Contact_UserCreated] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Contact_UserModified] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

