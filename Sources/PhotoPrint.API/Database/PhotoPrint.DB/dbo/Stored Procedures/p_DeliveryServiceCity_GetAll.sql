
CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[DeliveryServiceCity] e
END
