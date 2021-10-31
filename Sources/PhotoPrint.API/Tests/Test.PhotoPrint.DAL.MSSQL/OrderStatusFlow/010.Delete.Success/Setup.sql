

DECLARE @FromStatusID BIGINT = 9
DECLARE @ToStatusID BIGINT = 5
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[OrderStatusFlow]
				WHERE 
	(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN [FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN [ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[OrderStatusFlow]
		(
	 [FromStatusID],
	 [ToStatusID]
		)
	SELECT 		
			 @FromStatusID,
	 @ToStatusID
END

SELECT TOP 1 
	@FromStatusID = [FromStatusID], 
	@ToStatusID = [ToStatusID]
FROM 
	[dbo].[OrderStatusFlow] e
WHERE
	(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN [FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN [ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@FromStatusID, 
	@ToStatusID
