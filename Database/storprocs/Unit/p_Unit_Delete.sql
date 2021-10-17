


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_Delete]
GO

CREATE PROCEDURE [dbo].[p_Unit_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Unit]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[Unit]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
