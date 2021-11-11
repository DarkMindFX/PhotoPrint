
CREATE PROCEDURE [dbo].[p_Image_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Image] e
END
