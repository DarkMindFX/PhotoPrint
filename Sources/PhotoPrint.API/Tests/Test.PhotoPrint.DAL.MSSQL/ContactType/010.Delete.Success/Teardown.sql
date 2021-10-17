

DECLARE @ID BIGINT = NULL
DECLARE @ContactTypeName NVARCHAR(50) = 'ContactTypeName 7296c3dbe74545609b6d6a0d530ce811'
DECLARE @IsDeleted BIT = 1
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[ContactType]
				WHERE 
	IsDeleted = 0 AND

	(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN [ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[ContactType]
	WHERE 
	(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN [ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ContactType was not deleted', 1
END