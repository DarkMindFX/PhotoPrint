


DECLARE @ID BIGINT = NULL
DECLARE @UnitName NVARCHAR(50) = 'UnitName 750a867ec61b4d31a76437c45c1b4158'
DECLARE @UnitAbbr NVARCHAR(50) = 'UnitAbbr 750a867ec61b4d31a76437c45c1b4158'
DECLARE @Description NVARCHAR(1000) = 'Description 750a867ec61b4d31a76437c45c1b4158'
DECLARE @IsDeleted BIT = 0
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Unit]
				WHERE 
	(CASE WHEN @UnitName IS NOT NULL THEN (CASE WHEN [UnitName] = @UnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UnitAbbr IS NOT NULL THEN (CASE WHEN [UnitAbbr] = @UnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Unit]
		(
	 [UnitName],
	 [UnitAbbr],
	 [Description],
	 [IsDeleted]
		)
	SELECT 		
			 @UnitName,
	 @UnitAbbr,
	 @Description,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Unit] e
WHERE
	(CASE WHEN @UnitName IS NOT NULL THEN (CASE WHEN [UnitName] = @UnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UnitAbbr IS NOT NULL THEN (CASE WHEN [UnitAbbr] = @UnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
