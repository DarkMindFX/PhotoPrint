


DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100011
DECLARE @Url NVARCHAR(1000) = 'Url ecbe8875f5684e6b872306fed389bfac'
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[UserInteriorThumbnail]
				WHERE 

	1=1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[UserInteriorThumbnail]
	WHERE 
	1=1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN [Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1  

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserInteriorThumbnail was not deleted', 1
END