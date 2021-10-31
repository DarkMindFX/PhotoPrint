

DECLARE @ID BIGINT = NULL
DECLARE @MaterialTypeName NVARCHAR(50) = 'MaterialTypeName 9ee2fdeae8544ae3a4508890f95b78ca'
DECLARE @Description NVARCHAR(1000) = 'Description 9ee2fdeae8544ae3a4508890f95b78ca'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 9ee2fdeae8544ae3a4508890f95b78ca'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '10/1/2021 4:11:33 PM'
DECLARE @CreatedByID BIGINT = 100010
DECLARE @ModifiedDate DATETIME = '10/1/2021 4:11:33 PM'
DECLARE @ModifiedByID BIGINT = 100003
 

DELETE FROM [MaterialType]
FROM 
	[dbo].[MaterialType] e
WHERE
	(CASE WHEN @MaterialTypeName IS NOT NULL THEN (CASE WHEN [MaterialTypeName] = @MaterialTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN [ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
