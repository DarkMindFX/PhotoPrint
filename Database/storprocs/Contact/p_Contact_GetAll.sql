


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Contact_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Contact] e
END
GO