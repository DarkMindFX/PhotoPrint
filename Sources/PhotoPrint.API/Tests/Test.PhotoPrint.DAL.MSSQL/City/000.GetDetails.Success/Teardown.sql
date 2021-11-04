


DECLARE @ID BIGINT = NULL
DECLARE @CityName NVARCHAR(250) = 'CityName 9f6068e2688c442498f1ad79592b1cc4'
DECLARE @RegionID BIGINT = 2
DECLARE @IsDeleted BIT = 0
 

DELETE FROM [City]
FROM 
	[dbo].[City] e
WHERE
	(CASE WHEN @CityName IS NOT NULL THEN (CASE WHEN [CityName] = @CityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RegionID IS NOT NULL THEN (CASE WHEN [RegionID] = @RegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
