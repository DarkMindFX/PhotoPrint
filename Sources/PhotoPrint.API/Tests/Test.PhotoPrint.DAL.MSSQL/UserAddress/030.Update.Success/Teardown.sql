

-- original values --
DECLARE @UserID BIGINT = 100004
DECLARE @AddressID BIGINT = 100011
DECLARE @IsPrimary BIT = 0
 
-- updated values --

DECLARE @updUserID BIGINT = 100004
DECLARE @updAddressID BIGINT = 100011
DECLARE @updIsPrimary BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[UserAddress]
				WHERE 
	(CASE WHEN @updUserID IS NOT NULL THEN (CASE WHEN [UserID] = @updUserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @updAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @updIsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[UserAddress]
	WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[UserAddress]
	WHERE 
	(CASE WHEN @updUserID IS NOT NULL THEN (CASE WHEN [UserID] = @updUserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAddressID IS NOT NULL THEN (CASE WHEN [AddressID] = @updAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @updIsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserAddress was not updated', 1
END