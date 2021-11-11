
CREATE PROCEDURE [dbo].[p_UserConfirmation_Update]
			@ID BIGINT,
			@UserID BIGINT,
			@ConfirmationCode NVARCHAR(50),
			@Comfirmed BIT,
			@ExpiresDate DATETIME,
			@ConfirmationDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserConfirmation]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[UserConfirmation]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[ConfirmationCode] = IIF( @ConfirmationCode IS NOT NULL, @ConfirmationCode, [ConfirmationCode] ) ,
									[Comfirmed] = IIF( @Comfirmed IS NOT NULL, @Comfirmed, [Comfirmed] ) ,
									[ExpiresDate] = IIF( @ExpiresDate IS NOT NULL, @ExpiresDate, [ExpiresDate] ) ,
									[ConfirmationDate] = IIF( @ConfirmationDate IS NOT NULL, @ConfirmationDate, [ConfirmationDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserConfirmation was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserConfirmation] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN e.[ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN e.[Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN e.[ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN e.[ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
