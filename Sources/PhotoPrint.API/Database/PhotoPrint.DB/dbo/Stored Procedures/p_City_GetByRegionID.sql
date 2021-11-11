
CREATE PROCEDURE [dbo].[p_City_GetByRegionID]

	@RegionID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[City] c 
				WHERE
					[RegionID] = @RegionID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[City] e
		WHERE 
			[RegionID] = @RegionID	

	END
	ELSE
		SET @Found = 0;
END
