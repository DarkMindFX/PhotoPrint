

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Url NVARCHAR(1000) = 'Url c4f0daa0efdb4b17af326e67020e2abf'
DECLARE @Order INT = 109
DECLARE @ImageID BIGINT = 100015
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updUrl NVARCHAR(1000) = 'Url e7617b77ffc3473cb64c6b3f2cec497d'
DECLARE @updOrder INT = 720
DECLARE @updImageID BIGINT = 100044
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ImageThumbnail]
				WHERE 
	(CASE WHEN @updUrl IS NOT NULL THEN (CASE WHEN [Url] = @updUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOrder IS NOT NULL THEN (CASE WHEN [Order] = @updOrder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @updImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ImageThumbnail]
	WHERE 
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN [Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ImageThumbnail]
	WHERE 
	(CASE WHEN @updUrl IS NOT NULL THEN (CASE WHEN [Url] = @updUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOrder IS NOT NULL THEN (CASE WHEN [Order] = @updOrder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @updImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ImageThumbnail was not updated', 1
END