


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_Delete]
GO

CREATE PROCEDURE [dbo].[p_AddressType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[AddressType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[AddressType]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
