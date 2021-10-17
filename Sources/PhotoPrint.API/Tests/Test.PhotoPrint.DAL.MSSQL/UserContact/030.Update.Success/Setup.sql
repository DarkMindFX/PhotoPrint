

DECLARE @UserID BIGINT = 100006
DECLARE @ContactID BIGINT = 100021
DECLARE @IsPrimary BIT = 0
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[UserContact]
				WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[UserContact]
		(
	 [UserID],
	 [ContactID],
	 [IsPrimary]
		)
	SELECT 		
			 @UserID,
	 @ContactID,
	 @IsPrimary
END

SELECT TOP 1 
	@UserID = [UserID], 
	@ContactID = [ContactID]
FROM 
	[dbo].[UserContact] e
WHERE
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@UserID, 
	@ContactID
