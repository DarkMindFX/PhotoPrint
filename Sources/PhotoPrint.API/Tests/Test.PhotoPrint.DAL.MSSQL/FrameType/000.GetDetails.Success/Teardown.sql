


DECLARE @ID BIGINT = NULL
DECLARE @FrameTypeName NVARCHAR(50) = 'FrameTypeName d767ac90e7df4e85afcb7de69bc7cc68'
DECLARE @Description NVARCHAR(1000) = 'Description d767ac90e7df4e85afcb7de69bc7cc68'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl d767ac90e7df4e85afcb7de69bc7cc68'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '1/16/2021 7:47:39 AM'
DECLARE @CreatedByID BIGINT = 100008
DECLARE @ModifiedDate DATETIME = '11/27/2019 10:55:39 AM'
DECLARE @ModifiedByID BIGINT = 100002
 

DELETE FROM [FrameType]
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
