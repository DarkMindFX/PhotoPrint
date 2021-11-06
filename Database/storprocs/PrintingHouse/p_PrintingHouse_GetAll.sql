


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouse_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouse_GetAll]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouse_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouse] e
END
GO