


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @MountingTypeName NVARCHAR(50) = 'MountingTypeName 1e6120ebc04d4b6eb3d5c5da8824155e'
DECLARE @Description NVARCHAR(1000) = 'Description 1e6120ebc04d4b6eb3d5c5da8824155e'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 1e6120ebc04d4b6eb3d5c5da8824155e'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '2/12/2023 6:41:39 AM'
DECLARE @CreatedByID BIGINT = 732925
DECLARE @ModifiedDate DATETIME = '2/12/2023 6:41:39 AM'
DECLARE @ModifiedByID BIGINT = 732925
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updMountingTypeName NVARCHAR(50) = 'MountingTypeName 86571a37cb084e1cbc52422a3a65e611'
DECLARE @updDescription NVARCHAR(1000) = 'Description 86571a37cb084e1cbc52422a3a65e611'
DECLARE @updThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 86571a37cb084e1cbc52422a3a65e611'
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '2/12/2023 6:41:39 AM'
DECLARE @updCreatedByID BIGINT = 732925
DECLARE @updModifiedDate DATETIME = '2/12/2023 6:41:39 AM'
DECLARE @updModifiedByID BIGINT = 732925
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[MountingType]
				WHERE 
	(CASE WHEN @updMountingTypeName IS NOT NULL THEN (CASE WHEN [MountingTypeName] = @updMountingTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[MountingType]
	WHERE 
	(CASE WHEN @MountingTypeName IS NOT NULL THEN (CASE WHEN [MountingTypeName] = @MountingTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[MountingType]
	WHERE 
	(CASE WHEN @updMountingTypeName IS NOT NULL THEN (CASE WHEN [MountingTypeName] = @updMountingTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	THROW 51001, 'MountingType was not updated', 1
END