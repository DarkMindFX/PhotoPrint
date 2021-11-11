
/*
Usage:
EXEC dbo.p_Utils_EraseDeletedData
*/
CREATE PROCEDURE dbo.p_Utils_EraseDeletedData 
	
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @tblParams AS TABLE (
		[Order] INT,
		[Table] NVARCHAR(100)
	)

	DECLARE @table AS NVARCHAR(100) 

	INSERT INTO @tblParams	
	

	SELECT 1, 'Address' 	UNION
	SELECT 2, 'AddressType' 	UNION
	SELECT 3, 'Category' 	UNION
	SELECT 4, 'City' 	UNION
	SELECT 5, 'Contact' 	UNION
	SELECT 6, 'ContactType' 	UNION
	SELECT 7, 'Country' 	UNION
	SELECT 8, 'Currency' 	UNION
	SELECT 9, 'DeliveryService' 	UNION
	SELECT 10, 'FrameType' 	UNION
	SELECT 11, 'Image' 	UNION
	SELECT 12, 'Mat' 	UNION
	SELECT 13, 'MaterialType' 	UNION
	SELECT 14, 'MountingType' 	UNION
	SELECT 15, 'Order' 	UNION
	SELECT 16, 'OrderItem' 	UNION
	SELECT 17, 'OrderPaymentDetails' 	UNION
	SELECT 18, 'OrderStatus' 	UNION
	SELECT 19, 'PaymentMethod' 	UNION
	SELECT 20, 'PrintingHouse' 	UNION
	SELECT 21, 'Region' 	UNION
	SELECT 22, 'Size' 	UNION
	SELECT 23, 'Unit' 	UNION
	SELECT 25, 'UserStatus' 	UNION
	SELECT 26, 'UserType' 	


	DECLARE paramsCursor CURSOR FOR
	SELECT [Table] FROM @tblParams ORDER BY [Order]

	DECLARE @sql AS NVARCHAR(MAX)
	
	OPEN paramsCursor 

	FETCH NEXT FROM paramsCursor INTO @table

	WHILE @@FETCH_STATUS = 0
	BEGIN

		BEGIN TRY

			BEGIN TRANSACTION

			PRINT('======= ' + @table + ' =======')

			SET @sql = 'DELETE FROM dbo.[' + @table + '] WHERE IsDeleted = 1'
			PRINT(@sql)

			EXEC(@sql);

			FETCH NEXT FROM paramsCursor INTO @table

			COMMIT TRANSACTION

		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SELECT   
				ERROR_NUMBER() AS ErrorNumber  
			   ,ERROR_MESSAGE() AS ErrorMessage;
		END CATCH

	END

		

	CLOSE paramsCursor
	DEALLOCATE paramsCursor
END
