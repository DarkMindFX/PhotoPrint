




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_GetByPrintingHouseID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_GetByPrintingHouseID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetByPrintingHouseID]

	@PrintingHouseID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseContact] c 
				WHERE
					[PrintingHouseID] = @PrintingHouseID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseContact] e
		WHERE 
			[PrintingHouseID] = @PrintingHouseID	

	END
	ELSE
		SET @Found = 0;
END
GO