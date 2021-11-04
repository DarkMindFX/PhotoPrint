


DECLARE @ID BIGINT = NULL
DECLARE @ManagerID BIGINT = 100006
DECLARE @UserID BIGINT = 100007
DECLARE @ContactID BIGINT = 100001
DECLARE @DeliveryAddressID BIGINT = 100013
DECLARE @DeliveryServiceID BIGINT = 100005
DECLARE @Comments NVARCHAR(1000) = 'Comments 28f1434c00c34b26a755ffc798d29e00'
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '12/27/2021 10:28:39 PM'
DECLARE @CreatedByID BIGINT = 100009
DECLARE @ModifiedDate DATETIME = '12/27/2021 10:28:39 PM'
DECLARE @ModifiedByID BIGINT = 100005
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Order]
				WHERE 
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DeliveryAddressID IS NOT NULL THEN (CASE WHEN [DeliveryAddressID] = @DeliveryAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN [Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Order]
		(
	 [ManagerID],
	 [UserID],
	 [ContactID],
	 [DeliveryAddressID],
	 [DeliveryServiceID],
	 [Comments],
	 [IsDeleted],
	 [CreatedDate],
	 [CreatedByID],
	 [ModifiedDate],
	 [ModifiedByID]
		)
	SELECT 		
			 @ManagerID,
	 @UserID,
	 @ContactID,
	 @DeliveryAddressID,
	 @DeliveryServiceID,
	 @Comments,
	 @IsDeleted,
	 @CreatedDate,
	 @CreatedByID,
	 @ModifiedDate,
	 @ModifiedByID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Order] e
WHERE
	(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN [ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DeliveryAddressID IS NOT NULL THEN (CASE WHEN [DeliveryAddressID] = @DeliveryAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN [DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN [Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
