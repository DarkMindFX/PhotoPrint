
CREATE PROCEDURE [dbo].[p_ImageRelated_Delete]
		@ImageID BIGINT,	
		@RelatedImageID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ImageRelated]  
				WHERE 
							[ImageID] = @ImageID	AND
							[RelatedImageID] = @RelatedImageID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[ImageRelated] 
			WHERE 
						[ImageID] = @ImageID	AND
						[RelatedImageID] = @RelatedImageID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
