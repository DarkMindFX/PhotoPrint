


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DeliveryService_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_DeliveryService_GetAll]
GO

CREATE PROCEDURE [dbo].[p_DeliveryService_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[DeliveryService] e
END
GO