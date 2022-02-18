


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserInteriorThumbnail_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserInteriorThumbnail_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserInteriorThumbnail_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserInteriorThumbnail] e
END
GO