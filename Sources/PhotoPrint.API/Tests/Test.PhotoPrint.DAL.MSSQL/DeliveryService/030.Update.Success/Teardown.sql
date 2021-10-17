

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @DeliveryServiceName NVARCHAR(50) = 'DeliveryServiceName 0aec51cde2d74bb1b084f155747e9f'
DECLARE @Description NVARCHAR(1000) = 'Description 0aec51cde2d74bb1b084f155747e9f08'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '5/12/2019 10:51:48 AM'
DECLARE @CreatedByID BIGINT = 100001
DECLARE @ModifiedDate DATETIME = '6/12/2023 9:07:48 AM'
DECLARE @ModifiedByID BIGINT = 100003
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updDeliveryServiceName NVARCHAR(50) = 'DeliveryServiceName 311de72dfc024740bcd6aafeffa6df'
DECLARE @updDescription NVARCHAR(1000) = 'Description 311de72dfc024740bcd6aafeffa6df2d'
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '4/15/2023 2:30:48 PM'
DECLARE @updCreatedByID BIGINT = 100003
DECLARE @updModifiedDate DATETIME = '7/15/2023 10:04:48 AM'
DECLARE @updModifiedByID BIGINT = 100006
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[DeliveryService]
				WHERE 
	(CASE WHEN @updDeliveryServiceName IS NOT NULL THEN (CASE WHEN [DeliveryServiceName] = @updDeliveryServiceName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[DeliveryService]
	WHERE 
	(CASE WHEN @DeliveryServiceName IS NOT NULL THEN (CASE WHEN [DeliveryServiceName] = @DeliveryServiceName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
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
	[dbo].[DeliveryService]
	WHERE 
	(CASE WHEN @updDeliveryServiceName IS NOT NULL THEN (CASE WHEN [DeliveryServiceName] = @updDeliveryServiceName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'DeliveryService was not updated', 1
END