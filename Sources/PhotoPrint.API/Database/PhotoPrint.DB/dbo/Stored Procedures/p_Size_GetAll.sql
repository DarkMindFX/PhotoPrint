
CREATE PROCEDURE [dbo].[p_Size_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Size] e
END
