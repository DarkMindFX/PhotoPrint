
CREATE PROCEDURE [dbo].[p_ImageCategory_GetByCategoryID]

	@CategoryID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageCategory] c 
				WHERE
					[CategoryID] = @CategoryID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImageCategory] e
		WHERE 
			[CategoryID] = @CategoryID	

	END
	ELSE
		SET @Found = 0;
END
