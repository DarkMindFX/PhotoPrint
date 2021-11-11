CREATE TABLE [dbo].[Category] (
    [ID]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (50)   NOT NULL,
    [Description]  NVARCHAR (1000) NULL,
    [ParentID]     BIGINT          NULL,
    [IsDeleted]    BIT             CONSTRAINT [DF_Category_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]  DATETIME        NOT NULL,
    [CreatedByID]  BIGINT          NOT NULL,
    [ModifiedDate] DATETIME        NULL,
    [ModifiedByID] BIGINT          NULL,
    CONSTRAINT [PK_ImageCategory] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Category_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Category_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_ImageCategory_ImageCategory] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Category] ([ID])
);

