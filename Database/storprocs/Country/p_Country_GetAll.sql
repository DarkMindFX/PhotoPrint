


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Country_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Country_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Country_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Country] e
END
GO