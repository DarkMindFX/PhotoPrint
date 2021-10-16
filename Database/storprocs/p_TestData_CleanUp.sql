
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
	

	SELECT -1, 'OrderPaymentDetails', 'OrderPaymentDetails.csv'				UNION
	SELECT -1, 'OrderItem', 'OrderItem.csv'				UNION
	SELECT 0, 'Order', 'Order.csv'						UNION
	SELECT 1, 'DeliveryServiceCity', 'DeliveryServiceCity.csv'	UNION
	SELECT 2, 'DeliveryService', 'DeliveryService.csv'	UNION
	SELECT 3, 'UserContact', 'UserContact.csv'			UNION
	SELECT 4, 'UserAddress', 'UserAddress.csv'			UNION
	SELECT 5, 'PrintingHouseAddress', 'PrintingHouseAddresss.csv'		UNION
	SELECT 6, 'PrintingHouseContact', 'PrintingHouseContact.csv'		UNION
	SELECT 7, 'Address', 'Address.csv'					UNION
	SELECT 8, 'Contact', 'Contact.csv'					UNION
	SELECT 9, 'Size', 'Size.csv'						UNION
	SELECT 10, 'ImageCategory', 'ImageCategory.csv'		UNION
	SELECT 11, 'Category', 'Category.csv'				UNION	
	SELECT 12, 'FrameType', 'FrameType.csv'				UNION	
	SELECT 13, 'Mat', 'Mat.csv'							UNION
	SELECT 14, 'MaterialType', 'MaterialType.csv'		UNION
	SELECT 15, 'MountingType', 'MountingType.csv'		UNION
	SELECT 16, 'PrintingHouse', 'PrintingHouse.csv'		UNION
	SELECT 17, 'ImageRelated', 'ImageRelated.csv'		UNION
	SELECT 17, 'ImageThumbnail', 'ImageThumbnail.csv'	UNION
	SELECT 18, 'Image', 'Image.csv'						UNION
	SELECT 99, 'User', 'User.csv'						

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
