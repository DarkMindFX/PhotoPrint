

DECLARE @DeliveryServiceID BIGINT = 100006
DECLARE @CityID BIGINT = 2
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[DeliveryServiceCity]
				WHERE 
	(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN [CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[DeliveryServiceCity]
		(
	 [DeliveryServiceID],
	 [CityID]
		)
	SELECT 		
			 @DeliveryServiceID,
	 @CityID
END

SELECT TOP 1 
	@DeliveryServiceID = [DeliveryServiceID], 
	@CityID = [CityID]
FROM 
	[dbo].[DeliveryServiceCity] e
WHERE
	(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN [CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@DeliveryServiceID, 
	@CityID
