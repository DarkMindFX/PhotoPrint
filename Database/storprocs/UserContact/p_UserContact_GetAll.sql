

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserContact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserContact] e
END
GO