



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_Delete]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_Delete]
		@ImageID BIGINT,	
		@CategoryID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ImageCategory]  
				WHERE 
							[ImageID] = @ImageID	AND
							[CategoryID] = @CategoryID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[ImageCategory] 
			WHERE 
						[ImageID] = @ImageID	AND
						[CategoryID] = @CategoryID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
