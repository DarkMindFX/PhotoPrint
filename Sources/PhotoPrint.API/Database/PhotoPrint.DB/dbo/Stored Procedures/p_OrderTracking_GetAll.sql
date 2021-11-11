
CREATE PROCEDURE [dbo].[p_OrderTracking_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderTracking] e
END
