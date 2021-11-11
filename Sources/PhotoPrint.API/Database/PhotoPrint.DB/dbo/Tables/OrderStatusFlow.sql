CREATE TABLE [dbo].[OrderStatusFlow] (
    [FromStatusID] BIGINT NOT NULL,
    [ToStatusID]   BIGINT NOT NULL,
    CONSTRAINT [PK_OrderStatusFlow] PRIMARY KEY CLUSTERED ([FromStatusID] ASC, [ToStatusID] ASC),
    CONSTRAINT [FK_OrderStatusFlow_StatusFrom] FOREIGN KEY ([FromStatusID]) REFERENCES [dbo].[OrderStatus] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderStatusFlow_StatusTo] FOREIGN KEY ([ToStatusID]) REFERENCES [dbo].[OrderStatus] ([ID])
);

