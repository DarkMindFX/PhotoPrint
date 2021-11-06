





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_GetByDeliveryServiceID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_GetByDeliveryServiceID]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_GetByDeliveryServiceID]

	@DeliveryServiceID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DeliveryServiceCity] c 
				WHERE
					[DeliveryServiceID] = @DeliveryServiceID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DeliveryServiceCity] e
		WHERE 
			[DeliveryServiceID] = @DeliveryServiceID	

	END
	ELSE
		SET @Found = 0;
END
GO