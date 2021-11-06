


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ContactType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ContactType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ContactType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ContactType] e
END
GO