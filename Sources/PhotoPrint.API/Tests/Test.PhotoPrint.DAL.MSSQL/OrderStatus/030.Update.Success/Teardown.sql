

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @OrderStatusName NVARCHAR(50) = 'OrderStatusName 1774607d0cce4f9386a8d0fd75331941'
DECLARE @IsDeleted BIGINT = 928916
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updOrderStatusName NVARCHAR(50) = 'OrderStatusName a702ef209a0f4eb7a9fddb59af63cce9'
DECLARE @updIsDeleted BIGINT = 928916
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[OrderStatus]
				WHERE 
	(CASE WHEN @updOrderStatusName IS NOT NULL THEN (CASE WHEN [OrderStatusName] = @updOrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[OrderStatus]
	WHERE 
	(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN [OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[OrderStatus]
	WHERE 
	(CASE WHEN @updOrderStatusName IS NOT NULL THEN (CASE WHEN [OrderStatusName] = @updOrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OrderStatus was not updated', 1
END