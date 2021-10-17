

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderPaymentDetails_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderPaymentDetails_Insert]
GO

CREATE PROCEDURE [dbo].[p_OrderPaymentDetails_Insert]
			@ID BIGINT,
			@OrderID BIGINT,
			@PaymentMethodID BIGINT,
			@PaymentTransUID NVARCHAR(250),
			@PaymentDateTime DATETIME,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderPaymentDetails]
	SELECT 
		@OrderID,
		@PaymentMethodID,
		@PaymentTransUID,
		@PaymentDateTime,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderPaymentDetails] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentMethodID IS NOT NULL THEN (CASE WHEN e.[PaymentMethodID] = @PaymentMethodID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentTransUID IS NOT NULL THEN (CASE WHEN e.[PaymentTransUID] = @PaymentTransUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PaymentDateTime IS NOT NULL THEN (CASE WHEN e.[PaymentDateTime] = @PaymentDateTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO