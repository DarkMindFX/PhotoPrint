


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @SizeName NVARCHAR(50) = 'SizeName 86fd124bcbb04e4fae963d3ebabf146b'
DECLARE @Width INT = 206
DECLARE @Height INT = 206
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '3/25/2020 7:38:40 PM'
DECLARE @CreatedByID BIGINT = 100005
DECLARE @ModifiedDate DATETIME = '2/3/2023 5:25:40 AM'
DECLARE @ModifiedByID BIGINT = 100011
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updSizeName NVARCHAR(50) = 'SizeName 92a0c91e3d5e4ed5b097c3141a31d97c'
DECLARE @updWidth INT = 728
DECLARE @updHeight INT = 728
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '2/3/2023 5:25:40 AM'
DECLARE @updCreatedByID BIGINT = 100003
DECLARE @updModifiedDate DATETIME = '6/23/2020 3:11:40 PM'
DECLARE @updModifiedByID BIGINT = 100004
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Size]
				WHERE 
	(CASE WHEN @updSizeName IS NOT NULL THEN (CASE WHEN [SizeName] = @updSizeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updWidth IS NOT NULL THEN (CASE WHEN [Width] = @updWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updHeight IS NOT NULL THEN (CASE WHEN [Height] = @updHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Size]
	WHERE 
	(CASE WHEN @SizeName IS NOT NULL THEN (CASE WHEN [SizeName] = @SizeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN [Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN [Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[Size]
	WHERE 
	(CASE WHEN @updSizeName IS NOT NULL THEN (CASE WHEN [SizeName] = @updSizeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updWidth IS NOT NULL THEN (CASE WHEN [Width] = @updWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updHeight IS NOT NULL THEN (CASE WHEN [Height] = @updHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Size was not updated', 1
END