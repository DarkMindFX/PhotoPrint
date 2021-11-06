


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Category_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Category] e
END
GO