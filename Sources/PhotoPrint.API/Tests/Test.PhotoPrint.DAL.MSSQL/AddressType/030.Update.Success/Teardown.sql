

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeName NVARCHAR(50) = 'AddressTypeName 5d741386c44d44139a9f16f122c23366'
DECLARE @IsDeleted BIT = 0
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updAddressTypeName NVARCHAR(50) = 'AddressTypeName 593dcfb6484640089cae34d61f69f880'
DECLARE @updIsDeleted BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[AddressType]
				WHERE 
	(CASE WHEN @updAddressTypeName IS NOT NULL THEN (CASE WHEN [AddressTypeName] = @updAddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[AddressType]
	WHERE 
	(CASE WHEN @AddressTypeName IS NOT NULL THEN (CASE WHEN [AddressTypeName] = @AddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[AddressType]
	WHERE 
	(CASE WHEN @updAddressTypeName IS NOT NULL THEN (CASE WHEN [AddressTypeName] = @updAddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'AddressType was not updated', 1
END