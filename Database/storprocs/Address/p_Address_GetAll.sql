


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Address_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Address] e
END
GO