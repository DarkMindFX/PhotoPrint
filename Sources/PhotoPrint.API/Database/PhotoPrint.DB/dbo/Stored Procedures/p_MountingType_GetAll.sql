
CREATE PROCEDURE [dbo].[p_MountingType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[MountingType] e
END
