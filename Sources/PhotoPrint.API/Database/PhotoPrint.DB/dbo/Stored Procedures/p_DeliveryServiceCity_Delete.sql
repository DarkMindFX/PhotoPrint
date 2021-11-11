
CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_Delete]
		@DeliveryServiceID BIGINT,	
		@CityID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[DeliveryServiceCity]  
				WHERE 
							[DeliveryServiceID] = @DeliveryServiceID	AND
							[CityID] = @CityID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[DeliveryServiceCity] 
			WHERE 
						[DeliveryServiceID] = @DeliveryServiceID	AND
						[CityID] = @CityID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
