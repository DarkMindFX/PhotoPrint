
CREATE PROCEDURE [dbo].[p_UserConfirmation_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserConfirmation] e
END
