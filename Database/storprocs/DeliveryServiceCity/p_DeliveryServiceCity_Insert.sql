


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_Insert]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_Insert]
			@DeliveryServiceID BIGINT,
			@CityID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[DeliveryServiceCity]
	SELECT 
		@DeliveryServiceID,
		@CityID
	
	

	SELECT
		e.*
	FROM
		[dbo].[DeliveryServiceCity] e
	WHERE
				(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN e.[CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO