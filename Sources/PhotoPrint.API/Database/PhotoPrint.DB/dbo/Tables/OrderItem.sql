﻿CREATE TABLE [dbo].[OrderItem] (
    [ID]                 BIGINT          IDENTITY (1, 1) NOT NULL,
    [OrderID]            BIGINT          NOT NULL,
    [ImageID]            BIGINT          NOT NULL,
    [Width]              INT             NULL,
    [Height]             INT             NULL,
    [SizeID]             BIGINT          NULL,
    [FrameTypeID]        BIGINT          NOT NULL,
    [FrameSizeID]        BIGINT          NULL,
    [MatID]              BIGINT          NOT NULL,
    [MaterialTypeID]     BIGINT          NOT NULL,
    [MountingTypeID]     BIGINT          NOT NULL,
    [ItemCount]          INT             NOT NULL,
    [PriceAmountPerItem] DECIMAL (18, 2) NOT NULL,
    [PriceCurrencyID]    BIGINT          NOT NULL,
    [Comments]           NVARCHAR (1000) NOT NULL,
    [PrintingHouseID]    BIGINT          NULL,
    [IsDeleted]          BIT             CONSTRAINT [DF_OrderItem_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]        DATETIME        NOT NULL,
    [CreatedByID]        BIGINT          NOT NULL,
    [ModifiedDate]       DATETIME        NULL,
    [ModifiedByID]       BIGINT          NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OrderItem_CreatedByUser] FOREIGN KEY ([CreatedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_OrderItem_Currency] FOREIGN KEY ([PriceCurrencyID]) REFERENCES [dbo].[Currency] ([ID]),
    CONSTRAINT [FK_OrderItem_FrameSize] FOREIGN KEY ([FrameSizeID]) REFERENCES [dbo].[Size] ([ID]),
    CONSTRAINT [FK_OrderItem_FrameType] FOREIGN KEY ([FrameTypeID]) REFERENCES [dbo].[FrameType] ([ID]),
    CONSTRAINT [FK_OrderItem_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]),
    CONSTRAINT [FK_OrderItem_Mat] FOREIGN KEY ([MatID]) REFERENCES [dbo].[Mat] ([ID]),
    CONSTRAINT [FK_OrderItem_MaterialType] FOREIGN KEY ([MaterialTypeID]) REFERENCES [dbo].[MaterialType] ([ID]),
    CONSTRAINT [FK_OrderItem_ModifiedByUser] FOREIGN KEY ([ModifiedByID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_OrderItem_MountingType] FOREIGN KEY ([MountingTypeID]) REFERENCES [dbo].[MountingType] ([ID]),
    CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItem_PrintingHouse] FOREIGN KEY ([PrintingHouseID]) REFERENCES [dbo].[PrintingHouse] ([ID]),
    CONSTRAINT [FK_OrderItem_Size] FOREIGN KEY ([SizeID]) REFERENCES [dbo].[Size] ([ID])
);
