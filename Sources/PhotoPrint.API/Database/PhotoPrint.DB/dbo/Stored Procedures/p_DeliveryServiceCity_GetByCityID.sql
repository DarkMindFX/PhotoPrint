
CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_GetByCityID]

	@CityID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryServiceCity] c 
				WHERE
					[CityID] = @CityID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DeliveryServiceCity] e
		WHERE 
			[CityID] = @CityID	

	END
	ELSE
		SET @Found = 0;
END
