
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_Populate'))
   DROP PROC dbo.p_TestData_Populate
GO

/*
Usage:
1. From local file
EXEC p_TestData_Populate 'D:\Projects\PhotoPrint\Testing\TestData\'

2. From Azure Blob

CREATE MASTER KEY ENCRYPTION BY PASSWORD ='<Password>'

CREATE DATABASE SCOPED CREDENTIAL UploadPhotoPrintTestData
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = '<SAS for blob folder>';

CREATE EXTERNAL DATA SOURCE PhotoPrint_Azure_TestData
WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://photoprintstorage.blob.core.windows.net',
        CREDENTIAL = UploadPhotoPrintTestData
    );
GO 

EXEC p_TestData_Populate 'photoprintdb-test-data/', 'PhotoPrint_Azure_TestData'

DROP EXTERNAL DATA SOURCE PhotoPrint_Azure_TestData

DROP DATABASE SCOPED CREDENTIAL UploadPhotoPrintTestData

DROP MASTER KEY
*/
CREATE PROCEDURE p_TestData_Populate
	@RootFolder NVARCHAR(100),
	@DataSource NVARCHAR(100) = NULL
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
	SELECT 9, 'Category', 'Category.csv', 1							UNION
	SELECT 10, 'FrameType', 'FrameType.csv', 1						UNION
	SELECT 11, 'Mat', 'Mat.csv', 1									UNION
	SELECT 12, 'MaterialType', 'MaterialType.csv', 1				UNION
	SELECT 12, 'MountingType', 'MountingType.csv', 1				UNION
	SELECT 13, 'PrintingHouse', 'PrintingHouse.csv', 1				UNION
	SELECT 14, 'Image', 'Image.csv', 1								UNION
	SELECT 15, 'ImageCategory', 'ImageCategory.csv', 0				UNION
	SELECT 16, 'ImageRelated', 'ImageRelated.csv', 0				UNION
	SELECT 17, 'ImageThumbnail', 'ImageThumbnail.csv', 1			UNION
	SELECT 18, 'PrintingHouseAddress', 'PrintingHouseAddress.csv', 0	UNION
	SELECT 19, 'PrintingHouseContact', 'PrintingHouseContact.csv', 0	UNION
	SELECT 20, 'Order', 'Order.csv', 1								UNION
	SELECT 21, 'OrderItem', 'OrderItem.csv', 1						UNION
	SELECT 22, 'OrderPaymentDetails', 'OrderPaymentDetails.csv', 1	UNION
	SELECT 23, 'OrderStatusFlow', 'OrderStatusFlow.csv', 0			UNION
	SELECT 24, 'OrderTracking', 'OrderTracking.csv', 1				UNION
	SELECT 25, 'UserConfirmation', 'UserConfirmation.csv', 1		UNION
	SELECT 26, 'UserInteriorThumbnail', 'UserInteriorThumbnail.csv', 1		



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
			WITH (' +
			CASE
				WHEN @DataSource IS NOT NULL THEN 'DATA_SOURCE=''' + @DataSource + ''',' 
				ELSE ''
			END +
			'KEEPIDENTITY,
			FIRSTROW = 2,
			FIELDTERMINATOR = '','',
			ROWTERMINATOR=''0x0d0a'',
			BATCHSIZE=2500000);'

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


