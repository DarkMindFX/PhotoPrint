

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_GetAll]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[UserAddress] e
END
GO