

DECLARE @ID BIGINT = NULL
DECLARE @UserTypeName NVARCHAR(50) = 'UserTypeName 15d41e70e5e04c0e9623a1bc7cb4cca2'
DECLARE @IsDeleted BIT = 0
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[UserType]
				WHERE 
	(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[UserType]
		(
	 [UserTypeName],
	 [IsDeleted]
		)
	SELECT 		
			 @UserTypeName,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[UserType] e
WHERE
	(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
