

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @MatName NVARCHAR(50) = 'MatName 60fb797116ab44bc8e458ed6cd52cd52'
DECLARE @Description NVARCHAR(1000) = 'Description 60fb797116ab44bc8e458ed6cd52cd52'
DECLARE @ThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl 60fb797116ab44bc8e458ed6cd52cd52'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '2/14/2019 10:47:48 AM'
DECLARE @CreatedByID BIGINT = 100004
DECLARE @ModifiedDate DATETIME = '12/24/2021 8:34:48 PM'
DECLARE @ModifiedByID BIGINT = 100006
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updMatName NVARCHAR(50) = 'MatName d2884ed7886d48029e6648aa8b1904ad'
DECLARE @updDescription NVARCHAR(1000) = 'Description d2884ed7886d48029e6648aa8b1904ad'
DECLARE @updThumbnailUrl NVARCHAR(1000) = 'ThumbnailUrl d2884ed7886d48029e6648aa8b1904ad'
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '12/24/2021 8:34:48 PM'
DECLARE @updCreatedByID BIGINT = 100005
DECLARE @updModifiedDate DATETIME = '5/15/2019 6:21:48 AM'
DECLARE @updModifiedByID BIGINT = 100003
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Mat]
				WHERE 
	(CASE WHEN @updMatName IS NOT NULL THEN (CASE WHEN [MatName] = @updMatName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[Mat]
	WHERE 
	(CASE WHEN @MatName IS NOT NULL THEN (CASE WHEN [MatName] = @MatName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[Mat]
	WHERE 
	(CASE WHEN @updMatName IS NOT NULL THEN (CASE WHEN [MatName] = @updMatName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	THROW 51001, 'Mat was not updated', 1
END