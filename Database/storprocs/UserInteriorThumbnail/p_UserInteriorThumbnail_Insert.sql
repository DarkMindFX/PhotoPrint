


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserInteriorThumbnail_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserInteriorThumbnail_Insert]
GO

CREATE PROCEDURE [dbo].[p_UserInteriorThumbnail_Insert]
			@ID BIGINT,
			@UserID BIGINT,
			@Url NVARCHAR(1000)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserInteriorThumbnail]
	SELECT 
		@UserID,
		@Url
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserInteriorThumbnail] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN e.[Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO