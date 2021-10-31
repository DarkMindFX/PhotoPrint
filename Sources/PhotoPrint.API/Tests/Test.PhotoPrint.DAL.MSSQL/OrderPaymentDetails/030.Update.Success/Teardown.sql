

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100004
DECLARE @PaymentMethodID BIGINT = 1
DECLARE @PaymentTransUID NVARCHAR(250) = 'PaymentTransUID 5d7ef05bb18a477093751e088187d09e'
DECLARE @PaymentDateTime DATETIME = '2/8/2023 4:51:33 AM'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '2/8/2023 4:51:33 AM'
DECLARE @CreatedByID BIGINT = 100005
DECLARE @ModifiedDate DATETIME = '6/28/2020 2:38:33 PM'
DECLARE @ModifiedByID BIGINT = 100002
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updOrderID BIGINT = 100008
DECLARE @updPaymentMethodID BIGINT = 1
DECLARE @updPaymentTransUID NVARCHAR(250) = 'PaymentTransUID 3de9c5f71bcb42a8bfff42ce8e67143d'
DECLARE @updPaymentDateTime DATETIME = '6/28/2020 2:38:33 PM'
DECLARE @updIsDeleted BIT = 0
DECLARE @updCreatedDate DATETIME = '6/28/2020 2:38:33 PM'
DECLARE @updCreatedByID BIGINT = 100007
DECLARE @updModifiedDate DATETIME = '5/10/2023 12:24:33 AM'
DECLARE @updModifiedByID BIGINT = 100002
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[OrderPaymentDetails]
				WHERE 
	(CASE WHEN @updOrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @updOrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPaymentMethodID IS NOT NULL THEN (CASE WHEN [PaymentMethodID] = @updPaymentMethodID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPaymentTransUID IS NOT NULL THEN (CASE WHEN [PaymentTransUID] = @updPaymentTransUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPaymentDateTime IS NOT NULL THEN (CASE WHEN [PaymentDateTime] = @updPaymentDateTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
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

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[OrderPaymentDetails]
	WHERE 
	(CASE WHEN @updOrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @updOrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPaymentMethodID IS NOT NULL THEN (CASE WHEN [PaymentMethodID] = @updPaymentMethodID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPaymentTransUID IS NOT NULL THEN (CASE WHEN [PaymentTransUID] = @updPaymentTransUID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPaymentDateTime IS NOT NULL THEN (CASE WHEN [PaymentDateTime] = @updPaymentDateTime THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OrderPaymentDetails was not updated', 1
END