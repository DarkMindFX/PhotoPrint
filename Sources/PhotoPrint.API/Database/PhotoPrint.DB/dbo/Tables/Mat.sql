CREATE TABLE [dbo].[Mat] (
    [ID]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [MatName]      NVARCHAR (50)   NOT NULL,
    [Description]  NVARCHAR (1000) NULL,
    [ThumbnailUrl] NVARCHAR (1000) NOT NULL,
    [IsDeleted]    BIT             CONSTRAINT [DF_Mat_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]  DATETIME        NOT NULL,
    [CreatedByID]  BIGINT          NOT NULL,
    [ModifiedDate] DATETIME        NULL,
    [ModifiedByID] BIGINT          NULL,
    CONSTRAINT [PK_Mat] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Mat_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Mat_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

