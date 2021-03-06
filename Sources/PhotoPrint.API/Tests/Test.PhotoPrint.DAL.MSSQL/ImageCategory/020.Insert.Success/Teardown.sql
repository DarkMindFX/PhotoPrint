


DECLARE @ImageID BIGINT = 100016
DECLARE @CategoryID BIGINT = 100001
 
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ImageCategory]
				WHERE 
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN [CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[ImageCategory]
	WHERE 
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN [CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ImageCategory was not inserted', 1
END