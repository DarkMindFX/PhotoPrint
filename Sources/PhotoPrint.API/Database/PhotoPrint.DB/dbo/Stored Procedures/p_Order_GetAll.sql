
CREATE PROCEDURE [dbo].[p_Order_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Order] e
END
