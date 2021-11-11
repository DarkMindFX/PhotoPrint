
CREATE PROCEDURE [dbo].[p_Image_GetByPriceCurrencyID]

	@PriceCurrencyID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Image] c 
				WHERE
					[PriceCurrencyID] = @PriceCurrencyID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Image] e
		WHERE 
			[PriceCurrencyID] = @PriceCurrencyID	

	END
	ELSE
		SET @Found = 0;
END
