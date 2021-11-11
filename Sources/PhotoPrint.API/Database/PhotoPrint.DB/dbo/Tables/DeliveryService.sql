CREATE TABLE [dbo].[DeliveryService] (
    [ID]                  BIGINT          IDENTITY (1, 1) NOT NULL,
    [DeliveryServiceName] NVARCHAR (50)   NOT NULL,
    [Description]         NVARCHAR (1000) NULL,
    [IsDeleted]           BIT             CONSTRAINT [DF_DeliveryType_IsEnabled] DEFAULT ((0)) NOT NULL,
    [CreatedDate]         DATETIME        NOT NULL,
    [CreatedByID]         BIGINT          NOT NULL,
    [ModifiedDate]        DATETIME        NULL,
    [ModifiedByID]        BIGINT          NULL,
    CONSTRAINT [PK_DeliveryType] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DeliveryService_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_DeliveryService_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

