

DECLARE @ID BIGINT = NULL
DECLARE @ContactTypeName NVARCHAR(50) = 'ContactTypeName 02b03e2221f34effa78f3158fbc7c517'
DECLARE @IsDeleted BIT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ContactType]
				WHERE 
	(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN [ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ContactType]
		(
	 [ContactTypeName],
	 [IsDeleted]
		)
	SELECT 		
			 @ContactTypeName,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[ContactType] e
WHERE
	(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN [ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
