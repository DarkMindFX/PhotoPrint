
/**************** Creating DB ****************************/
DECLARE @dbname nvarchar(128)
SET @dbname = N'PhotoPrint'

IF (NOT EXISTS (SELECT name 
	FROM master.sys.databases 
	WHERE ('[' + [name] + ']' = @dbname 
	OR [name] = @dbname)))
BEGIN
	CREATE DATABASE PhotoPrint
END
GO
