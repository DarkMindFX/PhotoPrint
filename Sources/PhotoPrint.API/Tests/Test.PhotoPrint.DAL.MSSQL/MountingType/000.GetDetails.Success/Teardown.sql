


DECLARE @ID BIGINT = NULL
DECLARE @MountingTypeName NVARCHAR(50) = 'MountingTypeName 4a56ba4dd22d47359d2a6fed6b9f3ea0'
DECLARE @Description NVARCHAR(1000) = 'Description 4a56ba4dd22d47359d2a6fed6b9f3ea0'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 4a56ba4dd22d47359d2a6fed6b9f3ea0'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '2/12/2023 6:41:39 AM'
DECLARE @CreatedByID BIGINT = 732925
DECLARE @ModifiedDate DATETIME = '2/12/2023 6:41:39 AM'
DECLARE @ModifiedByID BIGINT = 732925
 

DELETE FROM [MountingType]
FROM 
	[dbo].[MountingType] e
WHERE
	(CASE WHEN @MountingTypeName IS NOT NULL THEN (CASE WHEN [MountingTypeName] = @MountingTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN [ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
