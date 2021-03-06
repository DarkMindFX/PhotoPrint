


DECLARE @ID BIGINT = NULL
DECLARE @RegionName NVARCHAR(50) = 'RegionName d086f21560294102ac7a6b3feadf269d'
DECLARE @CountryID BIGINT = 157
DECLARE @IsDeleted BIT = 0
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[Region]
				WHERE 
	IsDeleted = 0 AND

	1=1 AND
	(CASE WHEN @RegionName IS NOT NULL THEN (CASE WHEN [RegionName] = @RegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CountryID IS NOT NULL THEN (CASE WHEN [CountryID] = @CountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	1=1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[Region]
	WHERE 
	1=1 AND
	(CASE WHEN @RegionName IS NOT NULL THEN (CASE WHEN [RegionName] = @RegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @CountryID IS NOT NULL THEN (CASE WHEN [CountryID] = @CountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	1=1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Region was not deleted', 1
END