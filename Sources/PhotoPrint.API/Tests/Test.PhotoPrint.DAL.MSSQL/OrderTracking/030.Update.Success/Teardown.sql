

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100007
DECLARE @OrderStatusID BIGINT = 10
DECLARE @SetDate DATETIME = '3/8/2024 10:28:49 AM'
DECLARE @SetByID BIGINT = 100004
DECLARE @Comment NVARCHAR(1000) = 'Comment 84dbb81e22c941f7bbc5642df6715595'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updOrderID BIGINT = 100010
DECLARE @updOrderStatusID BIGINT = 3
DECLARE @updSetDate DATETIME = '10/25/2021 6:28:49 AM'
DECLARE @updSetByID BIGINT = 100008
DECLARE @updComment NVARCHAR(1000) = 'Comment ad04dfe0b03e4cae8a591c15dce85917'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[OrderTracking]
				WHERE 
	(CASE WHEN @updOrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @updOrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOrderStatusID IS NOT NULL THEN (CASE WHEN [OrderStatusID] = @updOrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSetDate IS NOT NULL THEN (CASE WHEN [SetDate] = @updSetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSetByID IS NOT NULL THEN (CASE WHEN [SetByID] = @updSetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updComment IS NOT NULL THEN (CASE WHEN [Comment] = @updComment THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[OrderTracking]
	WHERE 
	(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OrderStatusID IS NOT NULL THEN (CASE WHEN [OrderStatusID] = @OrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetDate IS NOT NULL THEN (CASE WHEN [SetDate] = @SetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SetByID IS NOT NULL THEN (CASE WHEN [SetByID] = @SetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[OrderTracking]
	WHERE 
	(CASE WHEN @updOrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @updOrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOrderStatusID IS NOT NULL THEN (CASE WHEN [OrderStatusID] = @updOrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSetDate IS NOT NULL THEN (CASE WHEN [SetDate] = @updSetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSetByID IS NOT NULL THEN (CASE WHEN [SetByID] = @updSetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updComment IS NOT NULL THEN (CASE WHEN [Comment] = @updComment THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OrderTracking was not updated', 1
END