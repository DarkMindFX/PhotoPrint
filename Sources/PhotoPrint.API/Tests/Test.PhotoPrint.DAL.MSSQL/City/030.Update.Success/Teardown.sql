

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @CityName NVARCHAR(250) = 'CityName 03aa49710ecd4a62864a5808a135a65c'
DECLARE @RegionID BIGINT = 5
DECLARE @IsDeleted BIT = 0
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCityName NVARCHAR(250) = 'CityName fc311312ecb54a49ad8c6df36471939c'
DECLARE @updRegionID BIGINT = 2
DECLARE @updIsDeleted BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[City]
				WHERE 
	(CASE WHEN @updCityName IS NOT NULL THEN (CASE WHEN [CityName] = @updCityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRegionID IS NOT NULL THEN (CASE WHEN [RegionID] = @updRegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[City]
	WHERE 
	(CASE WHEN @CityName IS NOT NULL THEN (CASE WHEN [CityName] = @CityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RegionID IS NOT NULL THEN (CASE WHEN [RegionID] = @RegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[City]
	WHERE 
	(CASE WHEN @updCityName IS NOT NULL THEN (CASE WHEN [CityName] = @updCityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updRegionID IS NOT NULL THEN (CASE WHEN [RegionID] = @updRegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'City was not updated', 1
END