
CREATE PROCEDURE [dbo].[p_ImageThumbnail_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageThumbnail] e
END
