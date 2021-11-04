


DECLARE @ID BIGINT = NULL
DECLARE @UserTypeName NVARCHAR(50) = 'UserTypeName deb079862daf4a039fa4c1cf769bd2e8'
DECLARE @IsDeleted BIT = 0
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[UserType]
				WHERE 
	IsDeleted = 0 AND

	1=1 AND
	(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	1=1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[UserType]
	WHERE 
	1=1 AND
	(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	1=1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserType was not deleted', 1
END