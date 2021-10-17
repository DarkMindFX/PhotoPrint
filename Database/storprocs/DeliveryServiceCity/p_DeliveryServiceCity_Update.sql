

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_Update]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_Update]
			@DeliveryServiceID BIGINT,
			@CityID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryServiceCity]
					WHERE 
												[DeliveryServiceID] = @DeliveryServiceID	AND
												[CityID] = @CityID	
							))
	BEGIN
		UPDATE [dbo].[DeliveryServiceCity]
		SET
									[DeliveryServiceID] = IIF( @DeliveryServiceID IS NOT NULL, @DeliveryServiceID, [DeliveryServiceID] ) ,
									[CityID] = IIF( @CityID IS NOT NULL, @CityID, [CityID] ) 
						WHERE 
												[DeliveryServiceID] = @DeliveryServiceID	AND
												[CityID] = @CityID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'DeliveryServiceCity was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[DeliveryServiceCity] e
	WHERE
				(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN e.[CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO