
CREATE PROCEDURE [dbo].[p_OrderStatus_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[OrderStatus]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
