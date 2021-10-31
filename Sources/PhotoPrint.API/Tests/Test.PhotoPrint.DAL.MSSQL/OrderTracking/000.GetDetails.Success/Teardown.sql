

DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100010
DECLARE @OrderStatusID BIGINT = 4
DECLARE @SetDate DATETIME = '1/11/2020 7:26:33 PM'
DECLARE @SetByID BIGINT = 100003
DECLARE @Comment NVARCHAR(1000) = 'Comment 3522cc3ace3641eaa4759f568ae9ef77'
 

DELETE FROM [OrderTracking]
FROM 
	[dbo].[OrderTracking] e
WHERE
	(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OrderStatusID IS NOT NULL THEN (CASE WHEN [OrderStatusID] = @OrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetDate IS NOT NULL THEN (CASE WHEN [SetDate] = @SetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetByID IS NOT NULL THEN (CASE WHEN [SetByID] = @SetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 
