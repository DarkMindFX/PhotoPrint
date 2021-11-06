



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_Delete]
GO

CREATE PROCEDURE [dbo].[p_FrameType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[FrameType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			UPDATE [dbo].[FrameType]
		SET IsDeleted = 1
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
