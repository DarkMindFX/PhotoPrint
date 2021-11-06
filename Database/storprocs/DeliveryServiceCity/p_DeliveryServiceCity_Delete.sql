



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_Delete]
GO

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
GO
