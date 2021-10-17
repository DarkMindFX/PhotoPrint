

DECLARE @ID BIGINT = NULL
DECLARE @Url NVARCHAR(1000) = 'Url f211862719c742ab81880e895bf370d2'
DECLARE @Order INT = 321
DECLARE @ImageID BIGINT = 100032
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[ImageThumbnail]
				WHERE 

	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN [Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[ImageThumbnail]
	WHERE 
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN [Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ImageThumbnail was not deleted', 1
END