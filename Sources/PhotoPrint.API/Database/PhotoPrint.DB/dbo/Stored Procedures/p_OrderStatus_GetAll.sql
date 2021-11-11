
CREATE PROCEDURE [dbo].[p_OrderStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderStatus] e
END
