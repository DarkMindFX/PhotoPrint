





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_GetByPrintingHouseID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_GetByPrintingHouseID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_GetByPrintingHouseID]

	@PrintingHouseID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseAddress] c 
				WHERE
					[PrintingHouseID] = @PrintingHouseID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseAddress] e
		WHERE 
			[PrintingHouseID] = @PrintingHouseID	

	END
	ELSE
		SET @Found = 0;
END
GO