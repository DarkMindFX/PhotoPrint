


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_Delete]
GO

CREATE PROCEDURE [dbo].[p_Contact_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Contact]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Contact]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
