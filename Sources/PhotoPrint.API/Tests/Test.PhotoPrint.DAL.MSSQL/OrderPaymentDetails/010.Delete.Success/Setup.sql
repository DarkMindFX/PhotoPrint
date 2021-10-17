

DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100011
DECLARE @PaymentMethodID BIGINT = 2
DECLARE @PaymentTransUID NVARCHAR(250) = 'PaymentTransUID d30e1973cb7f4b79aa5e4f71396d2279'
DECLARE @PaymentDateTime DATETIME = '6/10/2021 8:03:49 PM'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '6/10/2021 8:03:49 PM'
DECLARE @CreatedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '4/20/2024 5:50:49 AM'
DECLARE @ModifiedByID BIGINT = 100007
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[OrderPaymentDetails]
				WHERE 
	(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PaymentMethodID IS NOT NULL THEN (CASE WHEN [PaymentMethodID] = @PaymentMethodID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PaymentTransUID IS NOT NULL THEN (CASE WHEN [PaymentTransUID] = @PaymentTransUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PaymentDateTime IS NOT NULL THEN (CASE WHEN [PaymentDateTime] = @PaymentDateTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[OrderPaymentDetails]
		(
	 [OrderID],
	 [PaymentMethodID],
	 [PaymentTransUID],
	 [PaymentDateTime],
	 [IsDeleted],
	 [CreatedDate],
	 [CreatedByID],
	 [ModifiedDate],
	 [ModifiedByID]
		)
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
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[OrderPaymentDetails] e
WHERE
	(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PaymentMethodID IS NOT NULL THEN (CASE WHEN [PaymentMethodID] = @PaymentMethodID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PaymentTransUID IS NOT NULL THEN (CASE WHEN [PaymentTransUID] = @PaymentTransUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PaymentDateTime IS NOT NULL THEN (CASE WHEN [PaymentDateTime] = @PaymentDateTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
