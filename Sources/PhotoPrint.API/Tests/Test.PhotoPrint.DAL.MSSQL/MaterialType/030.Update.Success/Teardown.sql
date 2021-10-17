

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @MaterialTypeName NVARCHAR(50) = 'MaterialTypeName 2e3b317bd23d493fb0ef641714fd234c'
DECLARE @Description NVARCHAR(1000) = 'Description 2e3b317bd23d493fb0ef641714fd234c'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 2e3b317bd23d493fb0ef641714fd234c'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '3/30/2022 4:46:48 AM'
DECLARE @CreatedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '3/30/2022 4:46:48 AM'
DECLARE @ModifiedByID BIGINT = 100007
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updMaterialTypeName NVARCHAR(50) = 'MaterialTypeName a0a7eb608cfc4a729313a62a0ba671a4'
DECLARE @updDescription NVARCHAR(1000) = 'Description a0a7eb608cfc4a729313a62a0ba671a4'
DECLARE @updThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl a0a7eb608cfc4a729313a62a0ba671a4'
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '8/18/2019 2:33:48 PM'
DECLARE @updCreatedByID BIGINT = 100007
DECLARE @updModifiedDate DATETIME = '8/18/2019 2:33:48 PM'
DECLARE @updModifiedByID BIGINT = 100001
 

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