
CREATE PROCEDURE [dbo].[p_OrderTracking_GetByOrderStatusID]

	@OrderStatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderTracking] c 
				WHERE
					[OrderStatusID] = @OrderStatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderTracking] e
		WHERE 
			[OrderStatusID] = @OrderStatusID	

	END
	ELSE
		SET @Found = 0;
END
