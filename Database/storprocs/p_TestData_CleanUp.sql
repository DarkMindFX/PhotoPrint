
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_CleanUp'))
   DROP PROC dbo.p_TestData_CleanUp
GO

/*
Usage:
EXEC dbo.p_TestData_CleanUp
*/
CREATE PROCEDURE dbo.p_TestData_CleanUp 
	
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @tblParams AS TABLE (
		[Order] INT,
		[Table] NVARCHAR(100),
		[File]  NVARCHAR(100)
	)

	DECLARE @file AS NVARCHAR(100) 
	DECLARE @table AS NVARCHAR(100) 

	INSERT INTO @tblParams	
	

	SELECT 1, 'Category', 'Category.csv'				UNION
	SELECT 2, 'Contact', 'Contact.csv'					UNION
	SELECT 3, 'DeliveryServiceCity', 'DeliveryServiceCity.csv'	UNION
	SELECT 4, 'DeliveryService', 'DeliveryService.csv'	UNION
	SELECT 5, 'UserContact', 'UserContact.csv'			UNION
	SELECT 6, 'UserAddress', 'UserAddress.csv'			UNION
	SELECT 7, 'Address', 'Address.csv'					UNION
	SELECT 8, 'Size', 'Size.csv'						UNION
	SELECT 9, 'User', 'User.csv'						
		
					
	

	DECLARE paramsCursor CURSOR FOR
	SELECT [File], [Table] FROM @tblParams ORDER BY [Order]

	DECLARE @sql AS NVARCHAR(MAX)
	
	OPEN paramsCursor 

	BEGIN TRY

		BEGIN TRANSACTION

		FETCH NEXT FROM paramsCursor INTO @file, @table

		WHILE @@FETCH_STATUS = 0
		BEGIN

			PRINT('======= ' + @table + ' =======')

			SET @sql = 'DELETE FROM dbo.[' + @table + ']'
			PRINT(@sql)

			EXEC(@sql);

			FETCH NEXT FROM paramsCursor INTO @file, @table

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
