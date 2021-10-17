

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_GetAll]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseAddress] e
END
GO