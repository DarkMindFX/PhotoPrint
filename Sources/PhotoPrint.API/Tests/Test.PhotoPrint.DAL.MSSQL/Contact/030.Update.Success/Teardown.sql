

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @ContactTypeID BIGINT = 1
DECLARE @Title NVARCHAR(50) = 'Title 4a4c34502ac446b8a34a31ea16818ded'
DECLARE @Comment NVARCHAR(250) = 'Comment 4a4c34502ac446b8a34a31ea16818ded'
DECLARE @Value NVARCHAR(1000) = 'Value 4a4c34502ac446b8a34a31ea16818ded'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedByID BIGINT = 100005
DECLARE @CreatedDate DATETIME = '7/4/2020 12:36:32 PM'
DECLARE @ModifiedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '7/4/2020 12:36:32 PM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updContactTypeID BIGINT = 1
DECLARE @updTitle NVARCHAR(50) = 'Title 4e8fb92ad4334e25bf09d781d8af5023'
DECLARE @updComment NVARCHAR(250) = 'Comment 4e8fb92ad4334e25bf09d781d8af5023'
DECLARE @updValue NVARCHAR(1000) = 'Value 4e8fb92ad4334e25bf09d781d8af5023'
DECLARE @updIsDeleted BIT = 0
DECLARE @updCreatedByID BIGINT = 100002
DECLARE @updCreatedDate DATETIME = '5/14/2023 1:03:32 PM'
DECLARE @updModifiedByID BIGINT = 100003
DECLARE @updModifiedDate DATETIME = '10/1/2020 10:49:32 PM'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Contact]
				WHERE 
	(CASE WHEN @updContactTypeID IS NOT NULL THEN (CASE WHEN [ContactTypeID] = @updContactTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updComment IS NOT NULL THEN (CASE WHEN [Comment] = @updComment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updValue IS NOT NULL THEN (CASE WHEN [Value] = @updValue THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Contact]
	WHERE 
	(CASE WHEN @ContactTypeID IS NOT NULL THEN (CASE WHEN [ContactTypeID] = @ContactTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN [Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN [Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Contact]
	WHERE 
	(CASE WHEN @updContactTypeID IS NOT NULL THEN (CASE WHEN [ContactTypeID] = @updContactTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updComment IS NOT NULL THEN (CASE WHEN [Comment] = @updComment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updValue IS NOT NULL THEN (CASE WHEN [Value] = @updValue THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Contact was not updated', 1
END