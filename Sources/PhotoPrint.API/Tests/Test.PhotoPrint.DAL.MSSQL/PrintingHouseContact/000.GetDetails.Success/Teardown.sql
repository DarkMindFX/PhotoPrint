

DECLARE @PrintingHouseID BIGINT = 100003
DECLARE @ContactID BIGINT = 100014
DECLARE @IsPrimary BIT = 0
 

DELETE FROM [PrintingHouseContact]
FROM 
	[dbo].[PrintingHouseContact] e
WHERE
	(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
