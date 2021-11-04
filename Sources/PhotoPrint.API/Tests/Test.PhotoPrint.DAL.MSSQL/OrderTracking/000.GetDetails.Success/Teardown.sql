


DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100012
DECLARE @OrderStatusID BIGINT = 5
DECLARE @SetDate DATETIME = '2/11/2023 1:32:39 PM'
DECLARE @SetByID BIGINT = 100011
DECLARE @Comment NVARCHAR(1000) = 'Comment 53770ed6b42c488cbf3a909dd2929305'
 

DELETE FROM [OrderTracking]
FROM 
	[dbo].[OrderTracking] e
WHERE
	(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OrderStatusID IS NOT NULL THEN (CASE WHEN [OrderStatusID] = @OrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetDate IS NOT NULL THEN (CASE WHEN [SetDate] = @SetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetByID IS NOT NULL THEN (CASE WHEN [SetByID] = @SetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 
