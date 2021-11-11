
CREATE PROCEDURE [dbo].[p_AddressType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[AddressType] e
END
