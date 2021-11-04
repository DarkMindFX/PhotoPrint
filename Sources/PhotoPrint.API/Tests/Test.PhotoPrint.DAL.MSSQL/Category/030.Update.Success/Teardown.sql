


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @CategoryName NVARCHAR(50) = 'CategoryName ee906cf321564d2abf3d60573d553408'
DECLARE @Description NVARCHAR(1000) = 'Description ee906cf321564d2abf3d60573d553408'
DECLARE @ParentID BIGINT = 100004
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '8/29/2020 11:04:38 AM'
DECLARE @CreatedByID BIGINT = 100001
DECLARE @ModifiedDate DATETIME = '8/29/2020 11:04:38 AM'
DECLARE @ModifiedByID BIGINT = 100008
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCategoryName NVARCHAR(50) = 'CategoryName 2be173b81cac48dea5ee5f59bf14f99b'
DECLARE @updDescription NVARCHAR(1000) = 'Description 2be173b81cac48dea5ee5f59bf14f99b'
DECLARE @updParentID BIGINT = 100009
DECLARE @updIsDeleted BIT = 0
DECLARE @updCreatedDate DATETIME = '7/10/2023 8:51:38 PM'
DECLARE @updCreatedByID BIGINT = 100001
DECLARE @updModifiedDate DATETIME = '7/10/2023 8:51:38 PM'
DECLARE @updModifiedByID BIGINT = 100004
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Category]
				WHERE 
	(CASE WHEN @updCategoryName IS NOT NULL THEN (CASE WHEN [CategoryName] = @updCategoryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @updParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Category]
	WHERE 
	(CASE WHEN @CategoryName IS NOT NULL THEN (CASE WHEN [CategoryName] = @CategoryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @ParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[Category]
	WHERE 
	(CASE WHEN @updCategoryName IS NOT NULL THEN (CASE WHEN [CategoryName] = @updCategoryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updParentID IS NOT NULL THEN (CASE WHEN [ParentID] = @updParentID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Category was not updated', 1
END