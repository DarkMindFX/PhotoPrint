


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @StatusName NVARCHAR(50) = 'StatusName 1169652d0836489988fbd4e493f36133'
DECLARE @IsDeleted BIT = 1
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updStatusName NVARCHAR(50) = 'StatusName 611cac47669146d99c54ef9e21e86efe'
DECLARE @updIsDeleted BIT = 1
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[UserStatus]
				WHERE 
	(CASE WHEN @updStatusName IS NOT NULL THEN (CASE WHEN [StatusName] = @updStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[UserStatus]
	WHERE 
	(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN [StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[UserStatus]
	WHERE 
	(CASE WHEN @updStatusName IS NOT NULL THEN (CASE WHEN [StatusName] = @updStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserStatus was not updated', 1
END