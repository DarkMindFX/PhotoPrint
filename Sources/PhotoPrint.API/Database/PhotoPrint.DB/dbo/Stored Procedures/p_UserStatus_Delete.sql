
CREATE PROCEDURE [dbo].[p_UserStatus_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserStatus]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[UserStatus]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
