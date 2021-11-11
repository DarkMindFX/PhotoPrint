
CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_Delete]
		@PrintingHouseID BIGINT,	
		@AddressID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PrintingHouseAddress]  
				WHERE 
							[PrintingHouseID] = @PrintingHouseID	AND
							[AddressID] = @AddressID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[PrintingHouseAddress] 
			WHERE 
						[PrintingHouseID] = @PrintingHouseID	AND
						[AddressID] = @AddressID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
