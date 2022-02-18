


DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100009
DECLARE @Url NVARCHAR(1000) = 'Url a95b4a207dff4f1f83c05652210c9e7e'
 
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[UserInteriorThumbnail]
				WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[UserInteriorThumbnail]
	WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserInteriorThumbnail was not inserted', 1
END