
CREATE PROCEDURE [dbo].[p_UserAddress_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserAddress] e
END
