CREATE TABLE [dbo].[MountingType] (
    [ID]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [MountingTypeName] NVARCHAR (50)   NOT NULL,
    [Description]      NVARCHAR (1000) NOT NULL,
    [ThumbnailUrl]     NVARCHAR (1000) NOT NULL,
    [IsDeleted]        BIT             CONSTRAINT [DF_MountingType_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]      DATETIME        NOT NULL,
    [CreatedByID]      BIGINT          NOT NULL,
    [ModifiedDate]     DATETIME        NULL,
    [ModifiedByID]     BIGINT          NULL,
    CONSTRAINT [PK_MountingType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

