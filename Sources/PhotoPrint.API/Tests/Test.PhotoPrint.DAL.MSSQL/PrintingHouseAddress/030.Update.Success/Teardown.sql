

-- original values --
DECLARE @PrintingHouseID BIGINT = 100002
DECLARE @AddressID BIGINT = 100001
DECLARE @IsPrimary BIT = 1
 
-- updated values --

DECLARE @updPrintingHouseID BIGINT = 100002
DECLARE @updAddressID BIGINT = 100001
DECLARE @updIsPrimary BIT = 1
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[PrintingHouseAddress]
				WHERE 
	(CASE WHEN @updPrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @updPrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @updAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @updIsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[PrintingHouseAddress]
	WHERE 
	(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[PrintingHouseAddress]
	WHERE 
	(CASE WHEN @updPrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @updPrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @updAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @updIsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'PrintingHouseAddress was not updated', 1
END