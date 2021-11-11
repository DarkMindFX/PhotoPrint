
CREATE PROCEDURE [dbo].[p_Category_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Category] e
END
