
CREATE PROCEDURE [dbo].[p_Region_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Region] e
END
