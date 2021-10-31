

DECLARE @ID BIGINT = NULL
DECLARE @Url NVARCHAR(1000) = 'Url c4f0daa0efdb4b17af326e67020e2abf'
DECLARE @Order INT = 109
DECLARE @ImageID BIGINT = 100015
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ImageThumbnail]
				WHERE 
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN [Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ImageThumbnail]
		(
	 [Url],
	 [Order],
	 [ImageID]
		)
	SELECT 		
			 @Url,
	 @Order,
	 @ImageID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[ImageThumbnail] e
WHERE
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN [Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
