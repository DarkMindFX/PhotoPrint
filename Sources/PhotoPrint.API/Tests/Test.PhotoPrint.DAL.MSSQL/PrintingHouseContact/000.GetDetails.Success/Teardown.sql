


DECLARE @PrintingHouseID BIGINT = 100004
DECLARE @ContactID BIGINT = 100018
DECLARE @IsPrimary BIT = 1
 

DELETE FROM [PrintingHouseContact]
FROM 
	[dbo].[PrintingHouseContact] e
WHERE
	(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
