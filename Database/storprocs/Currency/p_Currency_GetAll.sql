


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Currency_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Currency] e
END
GO