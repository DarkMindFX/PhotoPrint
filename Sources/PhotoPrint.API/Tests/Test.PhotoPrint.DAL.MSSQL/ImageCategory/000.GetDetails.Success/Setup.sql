

DECLARE @ImageID BIGINT = 100018
DECLARE @CategoryID BIGINT = 100008
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ImageCategory]
				WHERE 
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN [CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ImageCategory]
		(
	 [ImageID],
	 [CategoryID]
		)
	SELECT 		
			 @ImageID,
	 @CategoryID
END

SELECT TOP 1 
	@ImageID = [ImageID], 
	@CategoryID = [CategoryID]
FROM 
	[dbo].[ImageCategory] e
WHERE
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN [CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ImageID, 
	@CategoryID
