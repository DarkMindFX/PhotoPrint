
CREATE PROCEDURE [dbo].[p_ContactType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ContactType] e
END
