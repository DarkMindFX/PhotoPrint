




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_GetByOrderStatusID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_GetByOrderStatusID]
GO

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
GO