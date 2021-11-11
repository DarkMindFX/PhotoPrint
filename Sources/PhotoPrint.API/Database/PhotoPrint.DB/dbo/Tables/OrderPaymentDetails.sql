CREATE TABLE [dbo].[OrderPaymentDetails] (
    [ID]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [OrderID]         BIGINT         NOT NULL,
    [PaymentMethodID] BIGINT         NOT NULL,
    [PaymentTransUID] NVARCHAR (250) NULL,
    [PaymentDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            CONSTRAINT [DF_OrderPaymentDetails_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]     DATETIME       NOT NULL,
    [CreatedByID]     BIGINT         NOT NULL,
    [ModifiedDate]    DATETIME       NULL,
    [ModifiedByID]    BIGINT         NULL,
    CONSTRAINT [PK_OrderPaymentDetails] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrderPaymentDetails_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_OrderPaymentDetails_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_OrderPaymentDetails_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderPaymentDetails_PaymentMethod] FOREIGN KEY ([PaymentMethodID]) REFERENCES [dbo].[PaymentMethod] ([ID])
);

