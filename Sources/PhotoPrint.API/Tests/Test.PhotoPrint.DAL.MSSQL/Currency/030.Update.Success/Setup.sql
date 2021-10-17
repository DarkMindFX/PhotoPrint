

DECLARE @ID BIGINT = NULL
DECLARE @ISO NVARCHAR(5) = 'ISO 6'
DECLARE @CurrencyName NVARCHAR(50) = 'CurrencyName 6d057544c28a4399b4f6e1bb09c561e0'
DECLARE @IsDeleted BIT = 0
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Currency]
				WHERE 
	(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN [ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CurrencyName IS NOT NULL THEN (CASE WHEN [CurrencyName] = @CurrencyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Currency]
		(
	 [ISO],
	 [CurrencyName],
	 [IsDeleted]
		)
	SELECT 		
			 @ISO,
	 @CurrencyName,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Currency] e
WHERE
	(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN [ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CurrencyName IS NOT NULL THEN (CASE WHEN [CurrencyName] = @CurrencyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
