

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @FrameTypeName NVARCHAR(50) = 'FrameTypeName 7ee0a5cf5e4941c597f07d8cdee1a933'
DECLARE @Description NVARCHAR(1000) = 'Description 7ee0a5cf5e4941c597f07d8cdee1a933'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 7ee0a5cf5e4941c597f07d8cdee1a933'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '3/25/2023 9:29:48 AM'
DECLARE @CreatedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '3/25/2023 9:29:48 AM'
DECLARE @ModifiedByID BIGINT = 100008
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updFrameTypeName NVARCHAR(50) = 'FrameTypeName 44bda55972ac4d5cb814db2670544b49'
DECLARE @updDescription NVARCHAR(1000) = 'Description 44bda55972ac4d5cb814db2670544b49'
DECLARE @updThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 44bda55972ac4d5cb814db2670544b49'
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '8/11/2020 9:56:48 AM'
DECLARE @updCreatedByID BIGINT = 100003
DECLARE @updModifiedDate DATETIME = '8/11/2020 9:56:48 AM'
DECLARE @updModifiedByID BIGINT = 100007
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[FrameType]
				WHERE 
	(CASE WHEN @updFrameTypeName IS NOT NULL THEN (CASE WHEN [FrameTypeName] = @updFrameTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updThumbnailUrl IS NOT NULL THEN (CASE WHEN [ThumbnailUrl] = @updThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
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

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[FrameType]
	WHERE 
	(CASE WHEN @updFrameTypeName IS NOT NULL THEN (CASE WHEN [FrameTypeName] = @updFrameTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updThumbnailUrl IS NOT NULL THEN (CASE WHEN [ThumbnailUrl] = @updThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'FrameType was not updated', 1
END