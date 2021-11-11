
CREATE PROCEDURE [dbo].[p_Address_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Address] e
END
