

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageThumbnail_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageThumbnail_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImageThumbnail_Insert]
			@ID BIGINT,
			@Url NVARCHAR(1000),
			@Order INT,
			@ImageID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImageThumbnail]
	SELECT 
		@Url,
		@Order,
		@ImageID
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImageThumbnail] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN e.[Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN e.[Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO