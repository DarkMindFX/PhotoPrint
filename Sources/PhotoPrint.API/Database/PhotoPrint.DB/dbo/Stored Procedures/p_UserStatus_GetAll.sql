
CREATE PROCEDURE [dbo].[p_UserStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserStatus] e
END
