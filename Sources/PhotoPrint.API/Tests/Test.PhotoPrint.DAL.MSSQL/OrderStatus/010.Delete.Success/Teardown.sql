


DECLARE @ID BIGINT = NULL
DECLARE @OrderStatusName NVARCHAR(50) = 'OrderStatusName 8d63a61cf5024b5d92b1da9dca78b827'
DECLARE @IsDeleted BIT = 1
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[OrderStatus]
				WHERE 
	IsDeleted = 0 AND

	1=1 AND
	(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN [OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	1=1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[OrderStatus]
	WHERE 
	1=1 AND
	(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN [OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	1=1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OrderStatus was not deleted', 1
END