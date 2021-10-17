

DECLARE @ID BIGINT = NULL
DECLARE @OrderStatusName NVARCHAR(50) = 'OrderStatusName 9e123f644f31448f93ce355af824253a'
DECLARE @IsDeleted BIGINT = 928916
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[OrderStatus]
				WHERE 
	IsDeleted = 0 AND

	(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN [OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[OrderStatus]
	WHERE 
	(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN [OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OrderStatus was not deleted', 1
END