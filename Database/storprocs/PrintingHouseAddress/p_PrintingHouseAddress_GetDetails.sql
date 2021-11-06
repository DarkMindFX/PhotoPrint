




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_GetDetails]
		@PrintingHouseID BIGINT,	
		@AddressID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseAddress] c 
				WHERE 
								[PrintingHouseID] = @PrintingHouseID	AND
								[AddressID] = @AddressID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseAddress] e
		WHERE 
								[PrintingHouseID] = @PrintingHouseID	AND
								[AddressID] = @AddressID	
				END
	ELSE
		SET @Found = 0;
END
GO