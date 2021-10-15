
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_Populate'))
   DROP PROC dbo.p_TestData_Populate
GO

/*
Usage:
EXEC p_TestData_Populate 'D:\Projects\PhotoPrint\TestData\'
*/
CREATE PROCEDURE p_TestData_Populate
	@RootFolder NVARCHAR(100)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @tblParams AS TABLE (
		[Order] INT,
		[Table] NVARCHAR(100),
		[File]  NVARCHAR(100),
		[HasIdentity] BIT
	)

	DECLARE @file AS NVARCHAR(100) 
	DECLARE @table AS NVARCHAR(100) 
	DECLARE @hasIdentity AS BIT 

	INSERT INTO @tblParams

	SELECT 1, 'DeliveryService', 'DeliveryService.csv', 1			UNION
	SELECT 2, 'DeliveryServiceCity', 'DeliveryServiceCity.csv', 0	UNION
	SELECT 3, 'User', 'User.csv', 1									UNION
	SELECT 4, 'Address', 'Address.csv', 1							UNION
	SELECT 5, 'UserAddress', 'UserAddress.csv', 0					UNION
	SELECT 6, 'Contact', 'Contact.csv', 1							UNION
	SELECT 7, 'UserContact', 'UserContact.csv', 0					UNION
	SELECT 8, 'Size', 'Size.csv', 1									UNION
	SELECT 9, 'Category', 'Category.csv', 1				

	DECLARE paramsCursor CURSOR FOR
	SELECT [File], [Table], [HasIdentity] FROM @tblParams ORDER BY [Order]

	
	DECLARE @Path AS NVARCHAR(MAX)
	DECLARE @sql AS NVARCHAR(MAX)
	
	OPEN paramsCursor 

	BEGIN TRY

		BEGIN TRANSACTION

		FETCH NEXT FROM paramsCursor INTO @file, @table, @hasIdentity

		WHILE @@FETCH_STATUS = 0
		BEGIN

			PRINT('======= ' + @file + ' -> ' + @table + ' =======')

			SELECT @Path = CONCAT(@RootFolder, @file)
			IF(@hasIdentity = 1) BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @table + '] ON;'

				PRINT(@sql)
				EXEC(@sql)
			END


			SET @sql = 'BULK INSERT dbo.[' + @table + ']
			FROM ''' + @Path + '''
			WITH (
			KEEPIDENTITY,
			FIRSTROW = 2,
			FIELDTERMINATOR = '','',
			ROWTERMINATOR=''\n'',
			BATCHSIZE=250000);'

			PRINT(@sql)
			EXEC(@sql)

			IF(@hasIdentity = 1) 
			BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @table + '] OFF;'

				PRINT(@sql)
				EXEC(@sql)
			END

			FETCH NEXT FROM paramsCursor INTO @file, @table, @hasIdentity

		END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT   
        ERROR_NUMBER() AS ErrorNumber  
       ,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH

	CLOSE paramsCursor
	DEALLOCATE paramsCursor
    
END
GO


