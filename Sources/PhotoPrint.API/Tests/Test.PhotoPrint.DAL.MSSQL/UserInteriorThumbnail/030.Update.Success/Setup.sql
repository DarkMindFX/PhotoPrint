


DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100003
DECLARE @Url NVARCHAR(1000) = 'Url b6d03c685f9e475287a9d5108d42d46c'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[UserInteriorThumbnail]
				WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[UserInteriorThumbnail]
		(
	 [UserID],
	 [Url]
		)
	SELECT 		
			 @UserID,
	 @Url
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[UserInteriorThumbnail] e
WHERE
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
