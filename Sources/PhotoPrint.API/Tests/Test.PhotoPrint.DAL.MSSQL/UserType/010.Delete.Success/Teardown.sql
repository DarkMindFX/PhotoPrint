

DECLARE @ID BIGINT = NULL
DECLARE @UserTypeName NVARCHAR(50) = 'UserTypeName 5a8272027208442f96ca8201c383da39'
DECLARE @IsDeleted BIT = 0
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[UserType]
				WHERE 
	IsDeleted = 0 AND

	(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[UserType]
	WHERE 
	(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserType was not deleted', 1
END