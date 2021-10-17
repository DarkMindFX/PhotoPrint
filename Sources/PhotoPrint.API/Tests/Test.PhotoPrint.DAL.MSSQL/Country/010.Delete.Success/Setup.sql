

DECLARE @ID BIGINT = NULL
DECLARE @CountryName NVARCHAR(50) = 'CountryName fa9d3e7d4af549eeb6245a2376ef5f73'
DECLARE @ISO NVARCHAR(5) = 'ISO f'
DECLARE @IsDeleted BIT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Country]
				WHERE 
	(CASE WHEN @CountryName IS NOT NULL THEN (CASE WHEN [CountryName] = @CountryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN [ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Country]
		(
	 [CountryName],
	 [ISO],
	 [IsDeleted]
		)
	SELECT 		
			 @CountryName,
	 @ISO,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Country] e
WHERE
	(CASE WHEN @CountryName IS NOT NULL THEN (CASE WHEN [CountryName] = @CountryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN [ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
