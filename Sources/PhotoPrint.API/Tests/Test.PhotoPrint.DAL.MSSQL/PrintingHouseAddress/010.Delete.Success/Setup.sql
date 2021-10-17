

DECLARE @PrintingHouseID BIGINT = 100001
DECLARE @AddressID BIGINT = 100011
DECLARE @IsPrimary BIT = 0
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[PrintingHouseAddress]
				WHERE 
	(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[PrintingHouseAddress]
		(
	 [PrintingHouseID],
	 [AddressID],
	 [IsPrimary]
		)
	SELECT 		
			 @PrintingHouseID,
	 @AddressID,
	 @IsPrimary
END

SELECT TOP 1 
	@PrintingHouseID = [PrintingHouseID], 
	@AddressID = [AddressID]
FROM 
	[dbo].[PrintingHouseAddress] e
WHERE
	(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@PrintingHouseID, 
	@AddressID
