CREATE TABLE [dbo].[OrderStatus] (
    [ID]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [OrderStatusName] NVARCHAR (50) NOT NULL,
    [IsDeleted]       BIT           CONSTRAINT [DF_OrderStatus_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED ([ID] ASC)
);

