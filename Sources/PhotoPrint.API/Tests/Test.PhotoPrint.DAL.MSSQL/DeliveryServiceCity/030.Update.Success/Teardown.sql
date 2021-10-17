

-- original values --
DECLARE @DeliveryServiceID BIGINT = 100002
DECLARE @CityID BIGINT = 10
 
-- updated values --

DECLARE @updDeliveryServiceID BIGINT = 100002
DECLARE @updCityID BIGINT = 10
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[DeliveryServiceCity]
				WHERE 
	(CASE WHEN @updDeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @updDeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCityID IS NOT NULL THEN (CASE WHEN [CityID] = @updCityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[DeliveryServiceCity]
	WHERE 
	(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN [CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[DeliveryServiceCity]
	WHERE 
	(CASE WHEN @updDeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @updDeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCityID IS NOT NULL THEN (CASE WHEN [CityID] = @updCityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'DeliveryServiceCity was not updated', 1
END