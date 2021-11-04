


DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100003
DECLARE @PaymentMethodID BIGINT = 2
DECLARE @PaymentTransUID NVARCHAR(250) = 'PaymentTransUID 6c39c216f22947609bb6e4c6beb4cf62'
DECLARE @PaymentDateTime DATETIME = '5/20/2020 9:05:39 PM'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '5/20/2020 9:05:39 PM'
DECLARE @CreatedByID BIGINT = 100007
DECLARE @ModifiedDate DATETIME = '4/1/2023 6:52:39 AM'
DECLARE @ModifiedByID BIGINT = 100005
 


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
