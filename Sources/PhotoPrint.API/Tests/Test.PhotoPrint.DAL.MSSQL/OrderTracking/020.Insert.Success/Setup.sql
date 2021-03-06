


DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100001
DECLARE @OrderStatusID BIGINT = 1
DECLARE @SetDate DATETIME = '8/9/2019 12:41:39 PM'
DECLARE @SetByID BIGINT = 100001
DECLARE @Comment NVARCHAR(1000) = 'Comment 4a13eb1c79b944dba6d4cdc6bc0f7293'
 


IF(EXISTS(SELECT 1 FROM 
					[dbo].[OrderTracking]
				WHERE 
	(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OrderStatusID IS NOT NULL THEN (CASE WHEN [OrderStatusID] = @OrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetDate IS NOT NULL THEN (CASE WHEN [SetDate] = @SetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetByID IS NOT NULL THEN (CASE WHEN [SetByID] = @SetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM [dbo].[OrderTracking]
WHERE 
	(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OrderStatusID IS NOT NULL THEN (CASE WHEN [OrderStatusID] = @OrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetDate IS NOT NULL THEN (CASE WHEN [SetDate] = @SetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetByID IS NOT NULL THEN (CASE WHEN [SetByID] = @SetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 

END