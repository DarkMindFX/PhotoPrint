
CREATE PROCEDURE [dbo].[p_DeliveryService_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[DeliveryService] e
END
