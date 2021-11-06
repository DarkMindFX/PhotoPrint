


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OrderPaymentDetails] e
END
GO