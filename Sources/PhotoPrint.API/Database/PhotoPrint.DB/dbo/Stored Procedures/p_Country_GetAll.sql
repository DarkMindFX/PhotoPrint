
CREATE PROCEDURE [dbo].[p_Country_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Country] e
END
