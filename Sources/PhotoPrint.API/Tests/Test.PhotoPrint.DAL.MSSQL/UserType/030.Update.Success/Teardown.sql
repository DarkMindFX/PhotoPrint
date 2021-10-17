

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @UserTypeName NVARCHAR(50) = 'UserTypeName 15d41e70e5e04c0e9623a1bc7cb4cca2'
DECLARE @IsDeleted BIT = 0
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updUserTypeName NVARCHAR(50) = 'UserTypeName 045872aad309441d8f596e48aac58ee3'
DECLARE @updIsDeleted BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[UserType]
				WHERE 
	(CASE WHEN @updUserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @updUserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[UserType]
	WHERE 
	(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[UserType]
	WHERE 
	(CASE WHEN @updUserTypeName IS NOT NULL THEN (CASE WHEN [UserTypeName] = @updUserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserType was not updated', 1
END