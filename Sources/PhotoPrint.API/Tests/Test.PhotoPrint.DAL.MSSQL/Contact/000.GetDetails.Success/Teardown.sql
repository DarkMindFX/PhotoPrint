


DECLARE @ID BIGINT = NULL
DECLARE @ContactTypeID BIGINT = 4
DECLARE @Title NVARCHAR(50) = 'Title 277a751ed8374a4b9b322c6012621517'
DECLARE @Comment NVARCHAR(250) = 'Comment 277a751ed8374a4b9b322c6012621517'
DECLARE @Value NVARCHAR(1000) = 'Value 277a751ed8374a4b9b322c6012621517'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedByID BIGINT = 100010
DECLARE @CreatedDate DATETIME = '12/15/2023 12:17:38 PM'
DECLARE @ModifiedByID BIGINT = 100007
DECLARE @ModifiedDate DATETIME = '5/4/2021 10:04:38 PM'
 

DELETE FROM [Contact]
FROM 
	[dbo].[Contact] e
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
