


DECLARE @ID BIGINT = NULL
DECLARE @UnitName NVARCHAR(50) = 'UnitName 73b370ab31be4df0965fafa171bbba85'
DECLARE @UnitAbbr NVARCHAR(50) = 'UnitAbbr 73b370ab31be4df0965fafa171bbba85'
DECLARE @Description NVARCHAR(1000) = 'Description 73b370ab31be4df0965fafa171bbba85'
DECLARE @IsDeleted BIT = 0
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[Unit]
				WHERE 
	IsDeleted = 0 AND

	1=1 AND
	(CASE WHEN @UnitName IS NOT NULL THEN (CASE WHEN [UnitName] = @UnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UnitAbbr IS NOT NULL THEN (CASE WHEN [UnitAbbr] = @UnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	1=1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[Unit]
	WHERE 
	1=1 AND
	(CASE WHEN @UnitName IS NOT NULL THEN (CASE WHEN [UnitName] = @UnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @UnitAbbr IS NOT NULL THEN (CASE WHEN [UnitAbbr] = @UnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	1=1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Unit was not deleted', 1
END