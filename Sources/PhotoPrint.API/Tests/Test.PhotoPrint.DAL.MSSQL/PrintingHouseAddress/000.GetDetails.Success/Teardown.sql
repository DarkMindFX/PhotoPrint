

DECLARE @PrintingHouseID BIGINT = 100002
DECLARE @AddressID BIGINT = 100008
DECLARE @IsPrimary BIT = 0
 

DELETE FROM [PrintingHouseAddress]
FROM 
	[dbo].[PrintingHouseAddress] e
WHERE
	(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
