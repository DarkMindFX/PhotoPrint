CREATE TABLE [dbo].[MaterialType] (
    [ID]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [MaterialTypeName] NVARCHAR (50)   NOT NULL,
    [Description]      NVARCHAR (1000) NOT NULL,
    [ThumbnailUrl]     NVARCHAR (1000) NOT NULL,
    [IsDeleted]        BIT             CONSTRAINT [DF_MaterialType_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]      DATETIME        NOT NULL,
    [CreatedByID]      BIGINT          NOT NULL,
    [ModifiedDate]     DATETIME        NULL,
    [ModifiedByID]     BIGINT          NULL,
    CONSTRAINT [PK_MaterialType] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_MaterialType_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_MaterialType_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

