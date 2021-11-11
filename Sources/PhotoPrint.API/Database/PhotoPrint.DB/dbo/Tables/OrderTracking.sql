CREATE TABLE [dbo].[OrderTracking] (
    [ID]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [OrderID]       BIGINT          NOT NULL,
    [OrderStatusID] BIGINT          NOT NULL,
    [SetDate]       DATETIME        NOT NULL,
    [SetByID]       BIGINT          NOT NULL,
    [Comment]       NVARCHAR (1000) NULL,
    CONSTRAINT [PK_OrderTracking] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrderTracking_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderTracking_OrderStatus] FOREIGN KEY ([OrderStatusID]) REFERENCES [dbo].[OrderStatus] ([ID]),
    CONSTRAINT [FK_OrderTracking_User] FOREIGN KEY ([SetByID]) REFERENCES [dbo].[User] ([ID])
);

