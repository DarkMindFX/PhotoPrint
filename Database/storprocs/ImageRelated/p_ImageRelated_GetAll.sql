

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImageRelated_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImageRelated_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImageRelated_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImageRelated] e
END
GO