


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @MaterialTypeName NVARCHAR(50) = 'MaterialTypeName b01c754554f04e14a8199f2ff5ac20f4'
DECLARE @Description NVARCHAR(1000) = 'Description b01c754554f04e14a8199f2ff5ac20f4'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl b01c754554f04e14a8199f2ff5ac20f4'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '3/18/2022 7:25:39 AM'
DECLARE @CreatedByID BIGINT = 100001
DECLARE @ModifiedDate DATETIME = '3/18/2022 7:25:39 AM'
DECLARE @ModifiedByID BIGINT = 100004
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updMaterialTypeName NVARCHAR(50) = 'MaterialTypeName ec14c354d5834b36aa92a129b97f7332'
DECLARE @updDescription NVARCHAR(1000) = 'Description ec14c354d5834b36aa92a129b97f7332'
DECLARE @updThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl ec14c354d5834b36aa92a129b97f7332'
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '3/18/2022 7:25:39 AM'
DECLARE @updCreatedByID BIGINT = 100009
DECLARE @updModifiedDate DATETIME = '8/5/2019 7:52:39 AM'
DECLARE @updModifiedByID BIGINT = 100010
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[MaterialType]
				WHERE 
	(CASE WHEN @updMaterialTypeName IS NOT NULL THEN (CASE WHEN [MaterialTypeName] = @updMaterialTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[MaterialType]
	WHERE 
	(CASE WHEN @MaterialTypeName IS NOT NULL THEN (CASE WHEN [MaterialTypeName] = @MaterialTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[MaterialType]
	WHERE 
	(CASE WHEN @updMaterialTypeName IS NOT NULL THEN (CASE WHEN [MaterialTypeName] = @updMaterialTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	THROW 51001, 'MaterialType was not updated', 1
END