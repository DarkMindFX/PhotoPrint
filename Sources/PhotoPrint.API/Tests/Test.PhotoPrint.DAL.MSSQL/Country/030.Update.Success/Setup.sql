

DECLARE @ID BIGINT = NULL
DECLARE @CountryName NVARCHAR(50) = 'CountryName ea4ff685f1dc4515ae4f4e1670886423'
DECLARE @ISO NVARCHAR(5) = 'ISO e'
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
