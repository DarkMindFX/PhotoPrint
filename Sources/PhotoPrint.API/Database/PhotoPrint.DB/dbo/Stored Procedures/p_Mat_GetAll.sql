
CREATE PROCEDURE [dbo].[p_Mat_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Mat] e
END
