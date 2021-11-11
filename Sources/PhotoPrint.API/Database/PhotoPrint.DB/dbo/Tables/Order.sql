CREATE TABLE [dbo].[Order] (
    [ID]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [ManagerID]         BIGINT          NULL,
    [UserID]            BIGINT          NOT NULL,
    [ContactID]         BIGINT          NOT NULL,
    [DeliveryAddressID] BIGINT          NOT NULL,
    [DeliveryServiceID] BIGINT          NOT NULL,
    [Comments]          NVARCHAR (1000) NULL,
    [IsDeleted]         BIT             CONSTRAINT [DF_Order_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]       DATETIME        NOT NULL,
    [CreatedByID]       BIGINT          NOT NULL,
    [ModifiedDate]      DATETIME        NULL,
    [ModifiedByID]      BIGINT          NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Order_AddressDelivery] FOREIGN KEY ([DeliveryAddressID]) REFERENCES [dbo].[Address] ([ID]),
    CONSTRAINT [FK_Order_Contact] FOREIGN KEY ([ContactID]) REFERENCES [dbo].[Contact] ([ID]),
    CONSTRAINT [FK_Order_DeliveryService] FOREIGN KEY ([DeliveryServiceID]) REFERENCES [dbo].[DeliveryService] ([ID]),
    CONSTRAINT [FK_Order_ManagerUser] FOREIGN KEY ([ManagerID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Order_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Order_UserCreated] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_Order_UserModified] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID])
);

