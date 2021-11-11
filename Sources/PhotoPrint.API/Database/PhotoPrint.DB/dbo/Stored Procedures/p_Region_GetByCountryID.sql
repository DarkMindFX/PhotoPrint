
CREATE PROCEDURE [dbo].[p_Region_GetByCountryID]

	@CountryID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Region] c 
				WHERE
					[CountryID] = @CountryID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Region] e
		WHERE 
			[CountryID] = @CountryID	

	END
	ELSE
		SET @Found = 0;
END
