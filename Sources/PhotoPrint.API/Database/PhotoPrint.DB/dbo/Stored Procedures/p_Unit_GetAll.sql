
CREATE PROCEDURE [dbo].[p_Unit_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Unit] e
END
