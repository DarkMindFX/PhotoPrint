
CREATE PROCEDURE [dbo].[p_Currency_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Currency] e
END
