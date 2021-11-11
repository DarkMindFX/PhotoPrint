CREATE TABLE [dbo].[Currency] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [ISO]          NVARCHAR (5)  NOT NULL,
    [CurrencyName] NVARCHAR (50) NOT NULL,
    [IsDeleted]    BIT           CONSTRAINT [DF_Currency_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([ID] ASC)
);

