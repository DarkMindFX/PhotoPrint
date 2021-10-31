

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @UnitName NVARCHAR(50) = 'UnitName 5d956188882f46ffa5cd6a96807ca207'
DECLARE @UnitAbbr NVARCHAR(50) = 'UnitAbbr 5d956188882f46ffa5cd6a96807ca207'
DECLARE @Description NVARCHAR(1000) = 'Description 5d956188882f46ffa5cd6a96807ca207'
DECLARE @IsDeleted BIT = 0
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updUnitName NVARCHAR(50) = 'UnitName ff0c9d35a0ba46049dfe8fbc84e3ea9c'
DECLARE @updUnitAbbr NVARCHAR(50) = 'UnitAbbr ff0c9d35a0ba46049dfe8fbc84e3ea9c'
DECLARE @updDescription NVARCHAR(1000) = 'Description ff0c9d35a0ba46049dfe8fbc84e3ea9c'
DECLARE @updIsDeleted BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Unit]
				WHERE 
	(CASE WHEN @updUnitName IS NOT NULL THEN (CASE WHEN [UnitName] = @updUnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUnitAbbr IS NOT NULL THEN (CASE WHEN [UnitAbbr] = @updUnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Unit]
	WHERE 
	(CASE WHEN @UnitName IS NOT NULL THEN (CASE WHEN [UnitName] = @UnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UnitAbbr IS NOT NULL THEN (CASE WHEN [UnitAbbr] = @UnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Unit]
	WHERE 
	(CASE WHEN @updUnitName IS NOT NULL THEN (CASE WHEN [UnitName] = @updUnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUnitAbbr IS NOT NULL THEN (CASE WHEN [UnitAbbr] = @updUnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Unit was not updated', 1
END