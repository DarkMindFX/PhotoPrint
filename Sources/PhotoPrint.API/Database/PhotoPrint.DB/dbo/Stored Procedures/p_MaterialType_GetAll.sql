
CREATE PROCEDURE [dbo].[p_MaterialType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[MaterialType] e
END
