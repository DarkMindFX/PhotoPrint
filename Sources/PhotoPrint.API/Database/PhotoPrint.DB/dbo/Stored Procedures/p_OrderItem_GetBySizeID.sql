
CREATE PROCEDURE [dbo].[p_OrderItem_GetBySizeID]

	@SizeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[SizeID] = @SizeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[SizeID] = @SizeID	

	END
	ELSE
		SET @Found = 0;
END
