




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Address_GetByCityID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Address_GetByCityID]
GO

CREATE PROCEDURE [dbo].[p_Address_GetByCityID]

	@CityID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Address] c 
				WHERE
					[CityID] = @CityID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Address] e
		WHERE 
			[CityID] = @CityID	

	END
	ELSE
		SET @Found = 0;
END
GO