


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_Insert]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_Insert]
			@PrintingHouseID BIGINT,
			@ContactID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PrintingHouseContact]
	SELECT 
		@PrintingHouseID,
		@ContactID,
		@IsPrimary
	
	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseContact] e
	WHERE
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO