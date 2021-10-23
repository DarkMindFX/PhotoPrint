

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @SizeName NVARCHAR(50) = 'SizeName 498795af76f34b5e8b3715d44ce5a1a9'
DECLARE @Width INT = 218
DECLARE @Height INT = 218
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '4/1/2020 4:20:49 AM'
DECLARE @CreatedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '2/10/2023 2:07:49 PM'
DECLARE @ModifiedByID BIGINT = 100008
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updSizeName NVARCHAR(50) = 'SizeName c97f2011693747218d3a68f4c1b849f1'
DECLARE @updWidth INT = 740
DECLARE @updHeight INT = 740
DECLARE @updIsDeleted BIT = 0
DECLARE @updCreatedDate DATETIME = '2/10/2023 2:07:49 PM'
DECLARE @updCreatedByID BIGINT = 100006
DECLARE @updModifiedDate DATETIME = '6/29/2020 2:34:49 PM'
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