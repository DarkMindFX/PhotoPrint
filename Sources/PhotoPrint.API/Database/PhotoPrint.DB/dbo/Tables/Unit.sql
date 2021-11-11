CREATE TABLE [dbo].[Unit] (
    [ID]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [UnitName]    NVARCHAR (50)   NOT NULL,
    [UnitAbbr]    NVARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (1000) NULL,
    [IsDeleted]   BIT             CONSTRAINT [DF_Unit_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED ([ID] ASC)
);

