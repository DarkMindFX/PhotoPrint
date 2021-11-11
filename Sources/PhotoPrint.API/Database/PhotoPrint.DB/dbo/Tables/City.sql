CREATE TABLE [dbo].[City] (
    [ID]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [CityName]  NVARCHAR (250) NOT NULL,
    [RegionID]  BIGINT         NOT NULL,
    [IsDeleted] BIT            CONSTRAINT [DF_City_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_City_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID])
);

