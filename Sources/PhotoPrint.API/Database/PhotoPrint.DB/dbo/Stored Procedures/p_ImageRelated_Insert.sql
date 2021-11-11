
CREATE PROCEDURE [dbo].[p_ImageRelated_Insert]
			@ImageID BIGINT,
			@RelatedImageID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImageRelated]
	SELECT 
		@ImageID,
		@RelatedImageID
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImageRelated] e
	WHERE
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RelatedImageID IS NOT NULL THEN (CASE WHEN e.[RelatedImageID] = @RelatedImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
