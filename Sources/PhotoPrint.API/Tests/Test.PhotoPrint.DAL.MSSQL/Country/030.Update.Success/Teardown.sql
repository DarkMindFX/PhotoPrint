

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @CountryName NVARCHAR(50) = 'CountryName 6d9a79a0da994f5d90a1203382d662c0'
DECLARE @ISO NVARCHAR(5) = 'ISO 6'
DECLARE @IsDeleted BIT = 0
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCountryName NVARCHAR(50) = 'CountryName dc9c86a313c64d979fd24e6e57a4c7a6'
DECLARE @updISO NVARCHAR(5) = 'ISO d'
DECLARE @updIsDeleted BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Country]
				WHERE 
	(CASE WHEN @updCountryName IS NOT NULL THEN (CASE WHEN [CountryName] = @updCountryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updISO IS NOT NULL THEN (CASE WHEN [ISO] = @updISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Country]
	WHERE 
	(CASE WHEN @CountryName IS NOT NULL THEN (CASE WHEN [CountryName] = @CountryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN [ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Country]
	WHERE 
	(CASE WHEN @updCountryName IS NOT NULL THEN (CASE WHEN [CountryName] = @updCountryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updISO IS NOT NULL THEN (CASE WHEN [ISO] = @updISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Country was not updated', 1
END