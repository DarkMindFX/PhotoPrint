


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderTracking] e
END
GO