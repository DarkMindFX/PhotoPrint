

-- original values --
DECLARE @ImageID BIGINT = 100026
DECLARE @RelatedImageID BIGINT = 100034
 
-- updated values --

DECLARE @updImageID BIGINT = 100026
DECLARE @updRelatedImageID BIGINT = 100034
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ImageRelated]
				WHERE 
	(CASE WHEN @updImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @updImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRelatedImageID IS NOT NULL THEN (CASE WHEN [RelatedImageID] = @updRelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ImageRelated]
	WHERE 
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN [RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ImageRelated]
	WHERE 
	(CASE WHEN @updImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @updImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRelatedImageID IS NOT NULL THEN (CASE WHEN [RelatedImageID] = @updRelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ImageRelated was not updated', 1
END