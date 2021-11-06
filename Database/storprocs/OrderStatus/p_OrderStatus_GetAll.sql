


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatus_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatus_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderStatus_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderStatus] e
END
GO