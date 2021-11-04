


DECLARE @DeliveryServiceID BIGINT = 100010
DECLARE @CityID BIGINT = 23
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[DeliveryServiceCity]
				WHERE 

	(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN [CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[DeliveryServiceCity]
	WHERE 
	(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN [CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1  

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'DeliveryServiceCity was not deleted', 1
END