
CREATE PROCEDURE [dbo].[p_PrintingHouseContact_Delete]
		@PrintingHouseID BIGINT,	
		@ContactID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[PrintingHouseContact]  
				WHERE 
							[PrintingHouseID] = @PrintingHouseID	AND
							[ContactID] = @ContactID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[PrintingHouseContact] 
			WHERE 
						[PrintingHouseID] = @PrintingHouseID	AND
						[ContactID] = @ContactID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
