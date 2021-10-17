

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserStatus_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserStatus_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserStatus] e
END
GO