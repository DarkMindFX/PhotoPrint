


DECLARE @ID BIGINT = NULL
DECLARE @SizeName NVARCHAR(50) = 'SizeName 7102ceab52464af0877533b065494de4'
DECLARE @Width INT = 219
DECLARE @Height INT = 219
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '4/20/2020 10:37:40 PM'
DECLARE @CreatedByID BIGINT = 100009
DECLARE @ModifiedDate DATETIME = '10/4/2022 6:14:40 PM'
DECLARE @ModifiedByID BIGINT = 100004
 

DELETE FROM [Size]
FROM 
	[dbo].[Size] e
WHERE
	(CASE WHEN @SizeName IS NOT NULL THEN (CASE WHEN [SizeName] = @SizeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN [Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN [Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
