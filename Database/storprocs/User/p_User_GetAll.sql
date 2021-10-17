

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetAll]
GO

CREATE PROCEDURE [dbo].[p_User_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[User] e
END
GO