
CREATE PROCEDURE [dbo].[p_UserContact_Update]
			@UserID BIGINT,
			@ContactID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserContact]
					WHERE 
												[UserID] = @UserID	AND
												[ContactID] = @ContactID	
							))
	BEGIN
		UPDATE [dbo].[UserContact]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[ContactID] = IIF( @ContactID IS NOT NULL, @ContactID, [ContactID] ) ,
									[IsPrimary] = IIF( @IsPrimary IS NOT NULL, @IsPrimary, [IsPrimary] ) 
						WHERE 
												[UserID] = @UserID	AND
												[ContactID] = @ContactID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserContact was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserContact] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
