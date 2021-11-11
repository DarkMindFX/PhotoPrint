CREATE TABLE [dbo].[FrameType] (
    [ID]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [FrameTypeName] NVARCHAR (50)   NOT NULL,
    [Description]   NVARCHAR (1000) NOT NULL,
    [ThumbnailUrl]  NVARCHAR (1000) NOT NULL,
    [IsDeleted]     BIT             CONSTRAINT [DF_FrameType_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]   DATETIME        NOT NULL,
    [CreatedByID]   BIGINT          NOT NULL,
    [ModifiedDate]  DATETIME        NULL,
    [ModifiedByID]  BIGINT          NULL,
    CONSTRAINT [PK_FrameType] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FrameType_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_FrameType_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

