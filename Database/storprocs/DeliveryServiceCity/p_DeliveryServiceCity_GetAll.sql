


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryServiceCity_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryServiceCity_GetAll]
GO

CREATE PROCEDURE [dbo].[p_DeliveryServiceCity_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[DeliveryServiceCity] e
END
GO