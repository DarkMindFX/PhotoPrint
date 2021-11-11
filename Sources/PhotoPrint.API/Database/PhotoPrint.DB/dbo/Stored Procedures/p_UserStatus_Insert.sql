
CREATE PROCEDURE [dbo].[p_UserStatus_Insert]
			@ID BIGINT,
			@StatusName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserStatus]
	SELECT 
		@StatusName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN e.[StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
