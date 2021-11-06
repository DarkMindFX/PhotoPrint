




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_GetDetails]
		@ImageID BIGINT,	
		@RelatedImageID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageRelated] c 
				WHERE 
								[ImageID] = @ImageID	AND
								[RelatedImageID] = @RelatedImageID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageRelated] e
		WHERE 
								[ImageID] = @ImageID	AND
								[RelatedImageID] = @RelatedImageID	
				END
	ELSE
		SET @Found = 0;
END
GO