
CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderPaymentDetails]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[OrderPaymentDetails] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
