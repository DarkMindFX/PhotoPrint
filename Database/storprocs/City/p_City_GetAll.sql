

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_GetAll]
GO

CREATE PROCEDURE [dbo].[p_City_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[City] e
END
GO