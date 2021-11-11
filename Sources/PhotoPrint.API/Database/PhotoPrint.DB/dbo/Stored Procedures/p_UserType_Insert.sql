
CREATE PROCEDURE [dbo].[p_UserType_Insert]
			@ID BIGINT,
			@UserTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserType]
	SELECT 
		@UserTypeName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN e.[UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
