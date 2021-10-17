

-- original values --
DECLARE @PrintingHouseID BIGINT = 100001
DECLARE @ContactID BIGINT = 100006
DECLARE @IsPrimary BIT = 0
 
-- updated values --

DECLARE @updPrintingHouseID BIGINT = 100001
DECLARE @updContactID BIGINT = 100006
DECLARE @updIsPrimary BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[PrintingHouseContact]
				WHERE 
	(CASE WHEN @updPrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @updPrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @updContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @updIsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[PrintingHouseContact]
	WHERE 
	(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[PrintingHouseContact]
	WHERE 
	(CASE WHEN @updPrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @updPrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @updContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @updIsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'PrintingHouseContact was not updated', 1
END