
CREATE PROCEDURE [dbo].[p_ImageRelated_GetByRelatedImageID]

	@RelatedImageID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageRelated] c 
				WHERE
					[RelatedImageID] = @RelatedImageID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageRelated] e
		WHERE 
			[RelatedImageID] = @RelatedImageID	

	END
	ELSE
		SET @Found = 0;
END
