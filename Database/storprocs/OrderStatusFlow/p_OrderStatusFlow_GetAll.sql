


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderStatusFlow] e
END
GO