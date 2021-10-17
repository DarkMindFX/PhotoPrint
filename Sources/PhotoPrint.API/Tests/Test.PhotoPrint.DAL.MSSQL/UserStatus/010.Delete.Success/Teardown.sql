

DECLARE @ID BIGINT = NULL
DECLARE @StatusName NVARCHAR(50) = 'StatusName db0b96241a9c4ddfbf8b3eaa04431ee2'
DECLARE @IsDeleted BIT = 1
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[UserStatus]
				WHERE 
	IsDeleted = 0 AND

	(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN [StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[UserStatus]
	WHERE 
	(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN [StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserStatus was not deleted', 1
END