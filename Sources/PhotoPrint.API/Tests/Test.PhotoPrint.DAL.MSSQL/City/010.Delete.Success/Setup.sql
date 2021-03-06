


DECLARE @ID BIGINT = NULL
DECLARE @CityName NVARCHAR(250) = 'CityName fd2c50d5ac634c069f087bf4fced57e7'
DECLARE @RegionID BIGINT = 18
DECLARE @IsDeleted BIT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[City]
				WHERE 
	(CASE WHEN @CityName IS NOT NULL THEN (CASE WHEN [CityName] = @CityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RegionID IS NOT NULL THEN (CASE WHEN [RegionID] = @RegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[City]
		(
	 [CityName],
	 [RegionID],
	 [IsDeleted]
		)
	SELECT 		
			 @CityName,
	 @RegionID,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[City] e
WHERE
	(CASE WHEN @CityName IS NOT NULL THEN (CASE WHEN [CityName] = @CityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RegionID IS NOT NULL THEN (CASE WHEN [RegionID] = @RegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
