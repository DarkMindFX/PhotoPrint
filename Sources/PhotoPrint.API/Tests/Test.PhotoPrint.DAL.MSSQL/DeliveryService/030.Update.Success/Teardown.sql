


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @DeliveryServiceName NVARCHAR(50) = 'DeliveryServiceName f01b59da68f140f5bc94f68fdebdbf'
DECLARE @Description NVARCHAR(1000) = 'Description f01b59da68f140f5bc94f68fdebdbf12'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '11/19/2021 3:44:38 PM'
DECLARE @CreatedByID BIGINT = 100006
DECLARE @ModifiedDate DATETIME = '4/10/2019 1:31:38 AM'
DECLARE @ModifiedByID BIGINT = 100003
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updDeliveryServiceName NVARCHAR(50) = 'DeliveryServiceName 6b8cebeb868f4c9584af4fabb4ce5d'
DECLARE @updDescription NVARCHAR(1000) = 'Description 6b8cebeb868f4c9584af4fabb4ce5ddd'
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedDate DATETIME = '2/18/2022 11:17:38 AM'
DECLARE @updCreatedByID BIGINT = 100003
DECLARE @updModifiedDate DATETIME = '2/18/2022 11:17:38 AM'
DECLARE @updModifiedByID BIGINT = 100011
 

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