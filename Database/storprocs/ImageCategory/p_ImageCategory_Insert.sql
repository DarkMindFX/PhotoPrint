

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_Insert]
			@ImageID BIGINT,
			@CategoryID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImageCategory]
	SELECT 
		@ImageID,
		@CategoryID
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImageCategory] e
	WHERE
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CategoryID IS NOT NULL THEN (CASE WHEN e.[CategoryID] = @CategoryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO