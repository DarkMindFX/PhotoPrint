

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_AddressType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[AddressType] e
END
GO