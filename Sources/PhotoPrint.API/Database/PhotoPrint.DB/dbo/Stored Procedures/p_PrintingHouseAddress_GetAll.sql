
CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseAddress] e
END
