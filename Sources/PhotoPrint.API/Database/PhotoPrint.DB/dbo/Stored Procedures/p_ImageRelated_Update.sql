
CREATE PROCEDURE [dbo].[p_ImageRelated_Update]
			@ImageID BIGINT,
			@RelatedImageID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageRelated]
					WHERE 
												[ImageID] = @ImageID	AND
												[RelatedImageID] = @RelatedImageID	
							))
	BEGIN
		UPDATE [dbo].[ImageRelated]
		SET
									[ImageID] = IIF( @ImageID IS NOT NULL, @ImageID, [ImageID] ) ,
									[RelatedImageID] = IIF( @RelatedImageID IS NOT NULL, @RelatedImageID, [RelatedImageID] ) 
						WHERE 
												[ImageID] = @ImageID	AND
												[RelatedImageID] = @RelatedImageID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImageRelated was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImageRelated] e
	WHERE
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN e.[RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
