CREATE TABLE [dbo].[Address] (
    [ID]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [AddressTypeID] BIGINT          NOT NULL,
    [Title]         NVARCHAR (50)   NOT NULL,
    [CityID]        BIGINT          NOT NULL,
    [Street]        NVARCHAR (50)   NOT NULL,
    [BuildingNo]    NVARCHAR (50)   NOT NULL,
    [ApartmentNo]   NVARCHAR (50)   NULL,
    [Comment]       NVARCHAR (1000) NULL,
    [CreatedByID]   BIGINT          NOT NULL,
    [CreatedDate]   DATETIME        NOT NULL,
    [ModifiedByID]  BIGINT          NULL,
    [ModifiedDate]  DATETIME        NULL,
    [IsDeleted]     BIT             CONSTRAINT [DF_Address_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Address_AddressType] FOREIGN KEY ([AddressTypeID]) REFERENCES [dbo].[AddressType] ([ID]),
    CONSTRAINT [FK_Address_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID]),
    CONSTRAINT [FK_Address_UserCreated] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Address_UserModified] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

