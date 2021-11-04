


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @RegionName NVARCHAR(50) = 'RegionName 88f270140a96428d8b05fad863dff015'
DECLARE @CountryID BIGINT = 248
DECLARE @IsDeleted BIT = 1
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updRegionName NVARCHAR(50) = 'RegionName 6aa1e92d35734f6cb45d3d6332f5f2ad'
DECLARE @updCountryID BIGINT = 83
DECLARE @updIsDeleted BIT = 1
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Region]
				WHERE 
	(CASE WHEN @updRegionName IS NOT NULL THEN (CASE WHEN [RegionName] = @updRegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCountryID IS NOT NULL THEN (CASE WHEN [CountryID] = @updCountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Region]
	WHERE 
	(CASE WHEN @RegionName IS NOT NULL THEN (CASE WHEN [RegionName] = @RegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CountryID IS NOT NULL THEN (CASE WHEN [CountryID] = @CountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Region]
	WHERE 
	(CASE WHEN @updRegionName IS NOT NULL THEN (CASE WHEN [RegionName] = @updRegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCountryID IS NOT NULL THEN (CASE WHEN [CountryID] = @updCountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Region was not updated', 1
END