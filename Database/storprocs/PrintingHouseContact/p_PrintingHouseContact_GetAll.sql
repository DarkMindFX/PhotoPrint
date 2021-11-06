


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_GetAll]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseContact] e
END
GO