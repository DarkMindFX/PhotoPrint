CREATE TABLE [dbo].[Size] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [SizeName]     NVARCHAR (50) NOT NULL,
    [Width]        INT           NOT NULL,
    [Height]       INT           NOT NULL,
    [IsDeleted]    BIT           CONSTRAINT [DF_Size_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL,
    [CreatedByID]  BIGINT        NOT NULL,
    [ModifiedDate] DATETIME      NULL,
    [ModifiedByID] BIGINT        NULL,
    CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Size_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Size_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

