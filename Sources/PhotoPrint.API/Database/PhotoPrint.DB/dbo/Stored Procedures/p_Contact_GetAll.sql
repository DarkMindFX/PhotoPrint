
CREATE PROCEDURE [dbo].[p_Contact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Contact] e
END
