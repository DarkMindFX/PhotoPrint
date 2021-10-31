

DECLARE @ImageID BIGINT = 100016
DECLARE @RelatedImageID BIGINT = 100019
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[ImageRelated]
				WHERE 
	IsDeleted = 0 AND

	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN [RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[ImageRelated]
	WHERE 
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		AND
	(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN [RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ImageRelated was not deleted', 1
END