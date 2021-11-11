CREATE TABLE [dbo].[Region] (
    [ID]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [RegionName] NVARCHAR (50) NOT NULL,
    [CountryID]  BIGINT        NOT NULL,
    [IsDeleted]  BIT           CONSTRAINT [DF_Region_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Region_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([ID]) ON DELETE CASCADE
);

