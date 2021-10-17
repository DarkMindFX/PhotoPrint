

-- original values --
DECLARE @ImageID BIGINT = 100019
DECLARE @CategoryID BIGINT = 100009
 
-- updated values --

DECLARE @updImageID BIGINT = 100019
DECLARE @updCategoryID BIGINT = 100009
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ImageCategory]
				WHERE 
	(CASE WHEN @updImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @updImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCategoryID IS NOT NULL THEN (CASE WHEN [CategoryID] = @updCategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ImageCategory]
	WHERE 
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN [CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ImageCategory]
	WHERE 
	(CASE WHEN @updImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @updImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCategoryID IS NOT NULL THEN (CASE WHEN [CategoryID] = @updCategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ImageCategory was not updated', 1
END