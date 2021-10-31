

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @CategoryName NVARCHAR(50) = 'CategoryName 39116a93dc2f453694e6a77c1dff1c6e'
DECLARE @Description NVARCHAR(1000) = 'Description 39116a93dc2f453694e6a77c1dff1c6e'
DECLARE @ParentID BIGINT = 100004
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '10/23/2020 1:10:32 PM'
DECLARE @CreatedByID BIGINT = 100011
DECLARE @ModifiedDate DATETIME = '9/2/2023 1:37:32 PM'
DECLARE @ModifiedByID BIGINT = 100004
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCategoryName NVARCHAR(50) = 'CategoryName 18bfb2c64d324bb6bbc31d0759ed1f7a'
DECLARE @updDescription NVARCHAR(1000) = 'Description 18bfb2c64d324bb6bbc31d0759ed1f7a'
DECLARE @updParentID BIGINT = 100004
DECLARE @updIsDeleted BIT = 0
DECLARE @updCreatedDate DATETIME = '2/29/2024 7:25:32 PM'
DECLARE @updCreatedByID BIGINT = 100004
DECLARE @updModifiedDate DATETIME = '7/20/2021 5:11:32 AM'
DECLARE @updModifiedByID BIGINT = 100002
 

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