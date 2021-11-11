CREATE TABLE [dbo].[ContactType] (
    [ID]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [ContactTypeName] NVARCHAR (50) NOT NULL,
    [IsDeleted]       BIT           CONSTRAINT [DF_ContactType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

