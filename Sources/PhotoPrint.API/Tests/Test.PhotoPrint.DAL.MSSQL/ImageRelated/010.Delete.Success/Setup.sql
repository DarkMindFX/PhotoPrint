

DECLARE @ImageID BIGINT = 100009
DECLARE @RelatedImageID BIGINT = 100026
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ImageRelated]
				WHERE 
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN [RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ImageRelated]
		(
	 [ImageID],
	 [RelatedImageID]
		)
	SELECT 		
			 @ImageID,
	 @RelatedImageID
END

SELECT TOP 1 
	@ImageID = [ImageID], 
	@RelatedImageID = [RelatedImageID]
FROM 
	[dbo].[ImageRelated] e
WHERE
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN [RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ImageID, 
	@RelatedImageID
