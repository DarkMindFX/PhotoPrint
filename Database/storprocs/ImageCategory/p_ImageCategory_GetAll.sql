


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageCategory_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageCategory_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImageCategory_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageCategory] e
END
GO