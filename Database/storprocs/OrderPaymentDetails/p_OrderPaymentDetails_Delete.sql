



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_Delete]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_Delete]
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
			UPDATE [dbo].[OrderPaymentDetails]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
