

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_Update]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_Update]
			@UserID BIGINT,
			@AddressID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserAddress]
					WHERE 
												[UserID] = @UserID	AND
												[AddressID] = @AddressID	
							))
	BEGIN
		UPDATE [dbo].[UserAddress]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[AddressID] = IIF( @AddressID IS NOT NULL, @AddressID, [AddressID] ) ,
									[IsPrimary] = IIF( @IsPrimary IS NOT NULL, @IsPrimary, [IsPrimary] ) 
						WHERE 
												[UserID] = @UserID	AND
												[AddressID] = @AddressID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserAddress was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserAddress] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN e.[AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO