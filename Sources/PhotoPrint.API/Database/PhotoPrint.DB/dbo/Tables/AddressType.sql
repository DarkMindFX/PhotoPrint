CREATE TABLE [dbo].[AddressType] (
    [ID]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [AddressTypeName] NVARCHAR (50) NOT NULL,
    [IsDeleted]       BIT           CONSTRAINT [DF_AddressType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

