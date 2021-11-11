
CREATE PROCEDURE [dbo].[p_City_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[City] e
END
