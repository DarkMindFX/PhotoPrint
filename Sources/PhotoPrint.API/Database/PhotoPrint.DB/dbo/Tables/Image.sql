CREATE TABLE [dbo].[Image] (
    [ID]              BIGINT          IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (50)   NOT NULL,
    [Description]     NVARCHAR (1000) NULL,
    [OriginUrl]       NVARCHAR (3000) NOT NULL,
    [MaxWidth]        INT             NULL,
    [MaxHeight]       INT             NULL,
    [PriceAmount]     DECIMAL (18, 2) NULL,
    [PriceCurrencyID] BIGINT          NULL,
    [IsDeleted]       BIT             CONSTRAINT [DF_Image_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedByID]     BIGINT          NOT NULL,
    [CreatedDate]     DATETIME        NOT NULL,
    [ModifiedByID]    BIGINT          NULL,
    [ModifiedDate]    DATETIME        NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Image_Currency] FOREIGN KEY ([PriceCurrencyID]) REFERENCES [dbo].[Currency] ([ID]),
    CONSTRAINT [FK_Image_UserCreated] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Image_UserModified] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

