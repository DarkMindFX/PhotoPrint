
CREATE PROCEDURE [dbo].[p_UserType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserType] e
END
