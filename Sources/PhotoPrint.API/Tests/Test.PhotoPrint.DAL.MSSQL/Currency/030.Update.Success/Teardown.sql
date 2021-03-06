


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @ISO NVARCHAR(5) = 'ISO 4'
DECLARE @CurrencyName NVARCHAR(50) = 'CurrencyName 4fe17efa547b4629a2868f61484d2c8a'
DECLARE @IsDeleted BIT = 0
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updISO NVARCHAR(5) = 'ISO 6'
DECLARE @updCurrencyName NVARCHAR(50) = 'CurrencyName 6003fad01ad54ad192b988f8450c0f2c'
DECLARE @updIsDeleted BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Currency]
				WHERE 
	(CASE WHEN @updISO IS NOT NULL THEN (CASE WHEN [ISO] = @updISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCurrencyName IS NOT NULL THEN (CASE WHEN [CurrencyName] = @updCurrencyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Currency]
	WHERE 
	(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN [ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CurrencyName IS NOT NULL THEN (CASE WHEN [CurrencyName] = @CurrencyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Currency]
	WHERE 
	(CASE WHEN @updISO IS NOT NULL THEN (CASE WHEN [ISO] = @updISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCurrencyName IS NOT NULL THEN (CASE WHEN [CurrencyName] = @updCurrencyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Currency was not updated', 1
END