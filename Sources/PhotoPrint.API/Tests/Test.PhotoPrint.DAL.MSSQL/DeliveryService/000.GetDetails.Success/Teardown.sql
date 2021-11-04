


DECLARE @ID BIGINT = NULL
DECLARE @DeliveryServiceName NVARCHAR(50) = 'DeliveryServiceName ca9920a03b15444d9f5855297cb362'
DECLARE @Description NVARCHAR(1000) = 'Description ca9920a03b15444d9f5855297cb362c6'
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '8/5/2023 6:41:38 AM'
DECLARE @CreatedByID BIGINT = 100008
DECLARE @ModifiedDate DATETIME = '5/7/2019 4:30:38 AM'
DECLARE @ModifiedByID BIGINT = 100005
 

DELETE FROM [DeliveryService]
FROM 
	[dbo].[DeliveryService] e
WHERE
	(CASE WHEN @DeliveryServiceName IS NOT NULL THEN (CASE WHEN [DeliveryServiceName] = @DeliveryServiceName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
