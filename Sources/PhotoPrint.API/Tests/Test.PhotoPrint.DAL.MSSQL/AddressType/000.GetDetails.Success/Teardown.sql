

DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeName NVARCHAR(50) = 'AddressTypeName 43f079185d234707ba0db64329ebbbf0'
DECLARE @IsDeleted BIT = 0
 

DELETE FROM [AddressType]
FROM 
	[dbo].[AddressType] e
WHERE
	(CASE WHEN @AddressTypeName IS NOT NULL THEN (CASE WHEN [AddressTypeName] = @AddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
