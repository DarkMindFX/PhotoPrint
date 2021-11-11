CREATE TABLE [dbo].[PrintingHouse] (
    [ID]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)   NOT NULL,
    [Description]  NVARCHAR (1000) NULL,
    [IsDeleted]    BIT             CONSTRAINT [DF_PrintingHouse_IsEnabled] DEFAULT ((0)) NOT NULL,
    [CreatedDate]  DATETIME        NOT NULL,
    [CreatedByID]  BIGINT          NOT NULL,
    [ModifiedDate] DATETIME        NULL,
    [ModifiedByID] BIGINT          NULL,
    CONSTRAINT [PK_PrintingHouse] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PrintingHouse_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_PrintingHouse_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

