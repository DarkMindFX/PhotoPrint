
CREATE PROCEDURE [dbo].[p_Address_GetByAddressTypeID]

	@AddressTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Address] c 
				WHERE
					[AddressTypeID] = @AddressTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Address] e
		WHERE 
			[AddressTypeID] = @AddressTypeID	

	END
	ELSE
		SET @Found = 0;
END
