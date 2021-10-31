

DECLARE @DeliveryServiceID BIGINT = 100005
DECLARE @CityID BIGINT = 6
 

DELETE FROM [DeliveryServiceCity]
FROM 
	[dbo].[DeliveryServiceCity] e
WHERE
	(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN [CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
