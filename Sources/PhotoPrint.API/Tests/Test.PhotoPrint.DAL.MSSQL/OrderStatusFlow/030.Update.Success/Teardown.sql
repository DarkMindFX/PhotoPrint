


-- original values --
DECLARE @FromStatusID BIGINT = 8
DECLARE @ToStatusID BIGINT = 8
 
-- updated values --

DECLARE @updFromStatusID BIGINT = 8
DECLARE @updToStatusID BIGINT = 8
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[OrderStatusFlow]
				WHERE 
	(CASE WHEN @updFromStatusID IS NOT NULL THEN (CASE WHEN [FromStatusID] = @updFromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updToStatusID IS NOT NULL THEN (CASE WHEN [ToStatusID] = @updToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[OrderStatusFlow]
	WHERE 
	(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN [FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN [ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[OrderStatusFlow]
	WHERE 
	(CASE WHEN @updFromStatusID IS NOT NULL THEN (CASE WHEN [FromStatusID] = @updFromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updToStatusID IS NOT NULL THEN (CASE WHEN [ToStatusID] = @updToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OrderStatusFlow was not updated', 1
END