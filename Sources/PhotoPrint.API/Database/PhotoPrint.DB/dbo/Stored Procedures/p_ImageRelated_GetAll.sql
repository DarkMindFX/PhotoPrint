
CREATE PROCEDURE [dbo].[p_ImageRelated_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageRelated] e
END
