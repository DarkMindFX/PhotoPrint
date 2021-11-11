
CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderPaymentDetails] e
END
