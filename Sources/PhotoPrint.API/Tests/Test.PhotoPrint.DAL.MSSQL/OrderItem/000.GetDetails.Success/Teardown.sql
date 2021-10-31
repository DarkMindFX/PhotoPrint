

DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100007
DECLARE @ImageID BIGINT = 100049
DECLARE @Width INT = 461
DECLARE @Height INT = 461
DECLARE @SizeID BIGINT = 100009
DECLARE @FrameTypeID BIGINT = 100009
DECLARE @FrameSizeID BIGINT = 100003
DECLARE @MatID BIGINT = 100001
DECLARE @MaterialTypeID BIGINT = 100001
DECLARE @MountingTypeID BIGINT = 100005
DECLARE @ItemCount INT = 163
DECLARE @PriceAmountPerItem DECIMAL(18, 2) = 163153.51
DECLARE @PriceCurrencyID BIGINT = 133
DECLARE @Comments NVARCHAR(1000) = 'Comments a4c589d9b7f443da81b23019d67f98c3'
DECLARE @PrintingHouseID BIGINT = 100002
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedDate DATETIME = '3/26/2020 6:26:33 AM'
DECLARE @CreatedByID BIGINT = 100003
DECLARE @ModifiedDate DATETIME = '3/26/2020 6:26:33 AM'
DECLARE @ModifiedByID BIGINT = 100008
 

DELETE FROM [OrderItem]
FROM 
	[dbo].[OrderItem] e
WHERE
	(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN [OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN [ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN [Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN [Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SizeID IS NOT NULL THEN (CASE WHEN [SizeID] = @SizeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FrameTypeID IS NOT NULL THEN (CASE WHEN [FrameTypeID] = @FrameTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FrameSizeID IS NOT NULL THEN (CASE WHEN [FrameSizeID] = @FrameSizeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MatID IS NOT NULL THEN (CASE WHEN [MatID] = @MatID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MaterialTypeID IS NOT NULL THEN (CASE WHEN [MaterialTypeID] = @MaterialTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MountingTypeID IS NOT NULL THEN (CASE WHEN [MountingTypeID] = @MountingTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ItemCount IS NOT NULL THEN (CASE WHEN [ItemCount] = @ItemCount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PriceAmountPerItem IS NOT NULL THEN (CASE WHEN [PriceAmountPerItem] = @PriceAmountPerItem THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN [PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN [Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN [PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
