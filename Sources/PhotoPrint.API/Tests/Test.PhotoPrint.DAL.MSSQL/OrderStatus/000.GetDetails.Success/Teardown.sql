


DECLARE @ID BIGINT = NULL
DECLARE @OrderStatusName NVARCHAR(50) = 'OrderStatusName 8505efd0a9e943799a639959488efd72'
DECLARE @IsDeleted BIT = 1
 

DELETE FROM [OrderStatus]
FROM 
	[dbo].[OrderStatus] e
WHERE
	(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN [OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
