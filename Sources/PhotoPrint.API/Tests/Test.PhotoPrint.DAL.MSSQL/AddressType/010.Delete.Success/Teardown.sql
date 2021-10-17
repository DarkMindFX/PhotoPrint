

DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeName NVARCHAR(50) = 'AddressTypeName 892913b0d7264db5808137fea0ce4f26'
DECLARE @IsDeleted BIT = 0
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[AddressType]
				WHERE 
	IsDeleted = 0 AND

	(CASE WHEN @AddressTypeName IS NOT NULL THEN (CASE WHEN [AddressTypeName] = @AddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[AddressType]
	WHERE 
	(CASE WHEN @AddressTypeName IS NOT NULL THEN (CASE WHEN [AddressTypeName] = @AddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'AddressType was not deleted', 1
END