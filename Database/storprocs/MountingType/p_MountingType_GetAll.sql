


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_MountingType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_MountingType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_MountingType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[MountingType] e
END
GO