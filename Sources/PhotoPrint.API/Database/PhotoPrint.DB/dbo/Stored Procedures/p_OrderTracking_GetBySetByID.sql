
CREATE PROCEDURE [dbo].[p_OrderTracking_GetBySetByID]

	@SetByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderTracking] c 
				WHERE
					[SetByID] = @SetByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderTracking] e
		WHERE 
			[SetByID] = @SetByID	

	END
	ELSE
		SET @Found = 0;
END
