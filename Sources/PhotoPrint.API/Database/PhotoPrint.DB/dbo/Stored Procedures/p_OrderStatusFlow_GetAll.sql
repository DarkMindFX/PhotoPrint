
CREATE PROCEDURE [dbo].[p_OrderStatusFlow_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderStatusFlow] e
END
