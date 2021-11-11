
CREATE PROCEDURE [dbo].[p_ImageCategory_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageCategory] e
END
