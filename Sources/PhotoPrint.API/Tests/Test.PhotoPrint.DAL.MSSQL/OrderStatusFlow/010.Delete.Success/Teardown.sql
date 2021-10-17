

DECLARE @FromStatusID BIGINT = 2
DECLARE @ToStatusID BIGINT = 11
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[OrderStatusFlow]
				WHERE 

	(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN [FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN [ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[OrderStatusFlow]
	WHERE 
	(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN [FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN [ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OrderStatusFlow was not deleted', 1
END