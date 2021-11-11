
CREATE PROCEDURE [dbo].[p_UserAddress_GetByAddressID]

	@AddressID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserAddress] c 
				WHERE
					[AddressID] = @AddressID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserAddress] e
		WHERE 
			[AddressID] = @AddressID	

	END
	ELSE
		SET @Found = 0;
END
