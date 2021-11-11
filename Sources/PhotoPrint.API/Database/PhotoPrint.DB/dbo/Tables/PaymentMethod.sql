CREATE TABLE [dbo].[PaymentMethod] (
    [ID]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (1000) NULL,
    [IsDeleted]   BIT             CONSTRAINT [DF_PaymentMethod_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED ([ID] ASC)
);

