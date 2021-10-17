

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100004
DECLARE @PaymentMethodID BIGINT = 3
DECLARE @PaymentTransUID NVARCHAR(250) = 'PaymentTransUID a7cec7e235954ce2ac5499dcc4950cb8'
DECLARE @PaymentDateTime DATETIME = '1/26/2019 4:03:49 PM'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '1/26/2019 4:03:49 PM'
DECLARE @CreatedByID BIGINT = 100006
DECLARE @ModifiedDate DATETIME = '12/7/2021 1:50:49 AM'
DECLARE @ModifiedByID BIGINT = 100004
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updOrderID BIGINT = 100001
DECLARE @updPaymentMethodID BIGINT = 4
DECLARE @updPaymentTransUID NVARCHAR(250) = 'PaymentTransUID 0bacd21fb916409d84f30e794666fae5'
DECLARE @updPaymentDateTime DATETIME = '4/27/2019 11:37:49 AM'
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '4/27/2019 11:37:49 AM'
DECLARE @updCreatedByID BIGINT = 100010
DECLARE @updModifiedDate DATETIME = '4/27/2019 11:37:49 AM'
DECLARE @updModifiedByID BIGINT = 100001
 

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