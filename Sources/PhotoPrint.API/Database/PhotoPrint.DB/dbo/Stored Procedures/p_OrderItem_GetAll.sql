
CREATE PROCEDURE [dbo].[p_OrderItem_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderItem] e
END
