

DECLARE @ID BIGINT = NULL
DECLARE @FrameTypeName NVARCHAR(50) = 'FrameTypeName 891f3c5eb61b46228a64669bd7c8b8d5'
DECLARE @Description NVARCHAR(1000) = 'Description 891f3c5eb61b46228a64669bd7c8b8d5'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 891f3c5eb61b46228a64669bd7c8b8d5'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '12/25/2022 1:55:48 PM'
DECLARE @CreatedByID BIGINT = 100007
DECLARE @ModifiedDate DATETIME = '12/25/2022 1:55:48 PM'
DECLARE @ModifiedByID BIGINT = 100004
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[FrameType]
				WHERE 
	(CASE WHEN @FrameTypeName IS NOT NULL THEN (CASE WHEN [FrameTypeName] = @FrameTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN [ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[FrameType]
		(
	 [FrameTypeName],
	 [Description],
	 [ThumbnailUrl],
	 [IsDeleted],
	 [CreatedDate],
	 [CreatedByID],
	 [ModifiedDate],
	 [ModifiedByID]
		)
	SELECT 		
			 @FrameTypeName,
	 @Description,
	 @ThumbnailUrl,
	 @IsDeleted,
	 @CreatedDate,
	 @CreatedByID,
	 @ModifiedDate,
	 @ModifiedByID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[FrameType] e
WHERE
	(CASE WHEN @FrameTypeName IS NOT NULL THEN (CASE WHEN [FrameTypeName] = @FrameTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN [ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
