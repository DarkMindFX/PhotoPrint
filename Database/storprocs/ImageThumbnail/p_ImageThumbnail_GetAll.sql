


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageThumbnail_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageThumbnail_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImageThumbnail_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageThumbnail] e
END
GO