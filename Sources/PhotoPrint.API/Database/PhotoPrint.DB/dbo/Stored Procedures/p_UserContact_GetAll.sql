
CREATE PROCEDURE [dbo].[p_UserContact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserContact] e
END
