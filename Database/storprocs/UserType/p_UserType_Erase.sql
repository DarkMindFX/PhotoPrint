




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserType_Erase', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserType_Erase]
GO

CREATE PROCEDURE [dbo].[p_UserType_Erase]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[UserType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
	
		DELETE 
		FROM 
			[dbo].[UserType] 	
		WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
