

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PaymentMethod_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_PaymentMethod_GetAll]
GO

CREATE PROCEDURE [dbo].[p_PaymentMethod_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[PaymentMethod] e
END
GO