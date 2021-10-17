




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_GetByPaymentMethodID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_GetByPaymentMethodID]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_GetByPaymentMethodID]

	@PaymentMethodID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderPaymentDetails] c 
				WHERE
					[PaymentMethodID] = @PaymentMethodID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderPaymentDetails] e
		WHERE 
			[PaymentMethodID] = @PaymentMethodID	

	END
	ELSE
		SET @Found = 0;
END
GO