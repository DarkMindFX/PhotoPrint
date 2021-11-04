


DECLARE @ID BIGINT = NULL
DECLARE @Url NVARCHAR(1000) = 'Url 7380c1c1473343079c6f9f31091a9c99'
DECLARE @Order INT = 203
DECLARE @ImageID BIGINT = 100041
 

DELETE FROM [ImageThumbnail]
FROM 
	[dbo].[ImageThumbnail] e
WHERE
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN [Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
