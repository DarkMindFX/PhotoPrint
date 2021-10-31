

DECLARE @UserID BIGINT = 100006
DECLARE @AddressID BIGINT = 100016
DECLARE @IsPrimary BIT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[UserAddress]
				WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[UserAddress]
		(
	 [UserID],
	 [AddressID],
	 [IsPrimary]
		)
	SELECT 		
			 @UserID,
	 @AddressID,
	 @IsPrimary
END

SELECT TOP 1 
	@UserID = [UserID], 
	@AddressID = [AddressID]
FROM 
	[dbo].[UserAddress] e
WHERE
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@UserID, 
	@AddressID
