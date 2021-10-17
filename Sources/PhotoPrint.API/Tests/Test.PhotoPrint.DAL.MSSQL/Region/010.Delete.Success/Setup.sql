

DECLARE @ID BIGINT = NULL
DECLARE @RegionName NVARCHAR(50) = 'RegionName 753c00c65b79445e84e8adf45a175787'
DECLARE @CountryID BIGINT = 112
DECLARE @IsDeleted BIGINT = 7022
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Region]
				WHERE 
	(CASE WHEN @RegionName IS NOT NULL THEN (CASE WHEN [RegionName] = @RegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CountryID IS NOT NULL THEN (CASE WHEN [CountryID] = @CountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Region]
		(
	 [RegionName],
	 [CountryID],
	 [IsDeleted]
		)
	SELECT 		
			 @RegionName,
	 @CountryID,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Region] e
WHERE
	(CASE WHEN @RegionName IS NOT NULL THEN (CASE WHEN [RegionName] = @RegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CountryID IS NOT NULL THEN (CASE WHEN [CountryID] = @CountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
