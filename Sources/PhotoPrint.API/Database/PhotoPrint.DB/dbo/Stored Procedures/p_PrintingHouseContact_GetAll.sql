
CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseContact] e
END
