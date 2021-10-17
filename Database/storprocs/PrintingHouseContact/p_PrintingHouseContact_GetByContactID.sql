




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_GetByContactID', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_GetByContactID]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetByContactID]

	@ContactID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseContact] c 
				WHERE
					[ContactID] = @ContactID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseContact] e
		WHERE 
			[ContactID] = @ContactID	

	END
	ELSE
		SET @Found = 0;
END
GO