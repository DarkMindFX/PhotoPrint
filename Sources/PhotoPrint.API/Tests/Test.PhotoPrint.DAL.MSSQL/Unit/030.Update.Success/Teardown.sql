


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @UnitName NVARCHAR(50) = 'UnitName 1204aec87d3145a6ad9719516f0dc184'
DECLARE @UnitAbbr NVARCHAR(50) = 'UnitAbbr 1204aec87d3145a6ad9719516f0dc184'
DECLARE @Description NVARCHAR(1000) = 'Description 1204aec87d3145a6ad9719516f0dc184'
DECLARE @IsDeleted BIT = 0
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updUnitName NVARCHAR(50) = 'UnitName 78663a6dedc042c585301831c29347db'
DECLARE @updUnitAbbr NVARCHAR(50) = 'UnitAbbr 78663a6dedc042c585301831c29347db'
DECLARE @updDescription NVARCHAR(1000) = 'Description 78663a6dedc042c585301831c29347db'
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