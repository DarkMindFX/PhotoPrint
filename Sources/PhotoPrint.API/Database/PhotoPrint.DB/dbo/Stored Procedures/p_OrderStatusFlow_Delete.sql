
CREATE PROCEDURE [dbo].[p_OrderStatusFlow_Delete]
		@FromStatusID BIGINT,	
		@ToStatusID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OrderStatusFlow]  
				WHERE 
							[FromStatusID] = @FromStatusID	AND
							[ToStatusID] = @ToStatusID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[OrderStatusFlow] 
			WHERE 
						[FromStatusID] = @FromStatusID	AND
						[ToStatusID] = @ToStatusID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
