




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetByOrderID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetByOrderID]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetByOrderID]

	@OrderID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderPaymentDetails] c 
				WHERE
					[OrderID] = @OrderID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderPaymentDetails] e
		WHERE 
			[OrderID] = @OrderID	

	END
	ELSE
		SET @Found = 0;
END
GO