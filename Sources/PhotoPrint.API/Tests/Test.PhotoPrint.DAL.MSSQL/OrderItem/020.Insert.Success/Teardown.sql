

DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100012
DECLARE @ImageID BIGINT = 100020
DECLARE @Width INT = 388
DECLARE @Height INT = 388
DECLARE @SizeID BIGINT = 100008
DECLARE @FrameTypeID BIGINT = 100006
DECLARE @FrameSizeID BIGINT = 100011
DECLARE @MatID BIGINT = 100002
DECLARE @MaterialTypeID BIGINT = 100001
DECLARE @MountingTypeID BIGINT = 100004
DECLARE @ItemCount INT = 359
DECLARE @PriceAmountPerItem DECIMAL(18, 2) = 555
DECLARE @PriceCurrencyID BIGINT = 208
DECLARE @Comments NVARCHAR(1000) = 'Comments 0ea424b8503641928e07fe541a1de985'
DECLARE @PrintingHouseID BIGINT = 100004
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '11/20/2023 3:02:49 AM'
DECLARE @CreatedByID BIGINT = 100009
DECLARE @ModifiedDate DATETIME = '4/8/2021 12:48:49 PM'
DECLARE @ModifiedByID BIGINT = 100003
 
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[OrderItem]
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
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[OrderItem]
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

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OrderItem was not inserted', 1
END