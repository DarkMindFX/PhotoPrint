


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Unit_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Unit] e
END
GO