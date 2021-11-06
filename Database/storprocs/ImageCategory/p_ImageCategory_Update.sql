


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_Update]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_Update]
			@ImageID BIGINT,
			@CategoryID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageCategory]
					WHERE 
												[ImageID] = @ImageID	AND
												[CategoryID] = @CategoryID	
							))
	BEGIN
		UPDATE [dbo].[ImageCategory]
		SET
									[ImageID] = IIF( @ImageID IS NOT NULL, @ImageID, [ImageID] ) ,
									[CategoryID] = IIF( @CategoryID IS NOT NULL, @CategoryID, [CategoryID] ) 
						WHERE 
												[ImageID] = @ImageID	AND
												[CategoryID] = @CategoryID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImageCategory was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImageCategory] e
	WHERE
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN e.[CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO