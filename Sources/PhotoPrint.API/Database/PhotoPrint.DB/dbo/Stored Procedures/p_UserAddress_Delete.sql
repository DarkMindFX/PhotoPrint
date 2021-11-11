
CREATE PROCEDURE [dbo].[p_UserAddress_Delete]
		@UserID BIGINT,	
		@AddressID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserAddress]  
				WHERE 
							[UserID] = @UserID	AND
							[AddressID] = @AddressID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[UserAddress] 
			WHERE 
						[UserID] = @UserID	AND
						[AddressID] = @AddressID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
