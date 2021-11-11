
CREATE PROCEDURE [dbo].[p_PrintingHouse_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouse] e
END
