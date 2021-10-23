

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @ContactTypeName NVARCHAR(50) = 'ContactTypeName e2439910c0864dec9e88226cece39d02'
DECLARE @IsDeleted BIT = 1
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updContactTypeName NVARCHAR(50) = 'ContactTypeName 7c68532bd17e4567b259161ff24f7b17'
DECLARE @updIsDeleted BIT = 1
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ContactType]
				WHERE 
	(CASE WHEN @updContactTypeName IS NOT NULL THEN (CASE WHEN [ContactTypeName] = @updContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ContactType]
	WHERE 
	(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN [ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ContactType]
	WHERE 
	(CASE WHEN @updContactTypeName IS NOT NULL THEN (CASE WHEN [ContactTypeName] = @updContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ContactType was not updated', 1
END