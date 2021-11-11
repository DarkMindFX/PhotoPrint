
CREATE PROCEDURE [dbo].[p_PaymentMethod_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PaymentMethod] e
END
