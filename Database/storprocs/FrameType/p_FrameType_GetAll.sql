

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_FrameType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[FrameType] e
END
GO