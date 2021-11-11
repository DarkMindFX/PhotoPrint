CREATE TABLE [dbo].[Country] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [CountryName] NVARCHAR (50) NOT NULL,
    [ISO]         NVARCHAR (5)  NOT NULL,
    [IsDeleted]   BIT           CONSTRAINT [DF_Country_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([ID] ASC)
);

