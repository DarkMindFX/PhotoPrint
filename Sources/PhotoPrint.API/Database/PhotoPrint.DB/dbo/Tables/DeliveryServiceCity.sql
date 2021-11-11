CREATE TABLE [dbo].[DeliveryServiceCity] (
    [DeliveryServiceID] BIGINT NOT NULL,
    [CityID]            BIGINT NOT NULL,
    CONSTRAINT [PK_DeliveryServiceCity_1] PRIMARY KEY CLUSTERED ([DeliveryServiceID] ASC, [CityID] ASC),
    CONSTRAINT [FK_DeliveryServiceCity_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_DeliveryServiceCity_DeliveryService] FOREIGN KEY ([DeliveryServiceID]) REFERENCES [dbo].[DeliveryService] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

