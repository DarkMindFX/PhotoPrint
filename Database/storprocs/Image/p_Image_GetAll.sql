

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Image_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Image] e
END
GO