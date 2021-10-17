

DECLARE @ID BIGINT = NULL
DECLARE @OrderID BIGINT = 100007
DECLARE @ImageID BIGINT = 100003
DECLARE @Width INT = 449
DECLARE @Height INT = 449
DECLARE @SizeID BIGINT = 100010
DECLARE @FrameTypeID BIGINT = 100008
DECLARE @FrameSizeID BIGINT = 100006
DECLARE @MatID BIGINT = 100001
DECLARE @MaterialTypeID BIGINT = 100004
DECLARE @MountingTypeID BIGINT = 100001
DECLARE @ItemCount INT = 17
DECLARE @PriceAmountPerItem DECIMAL(18, 2) = 555
DECLARE @PriceCurrencyID BIGINT = 179
DECLARE @Comments NVARCHAR(1000) = 'Comments 2810cab17aaf408e8481a26c0f6517af'
DECLARE @PrintingHouseID BIGINT = 100001
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedDate DATETIME = '1/3/2022 4:49:49 AM'
DECLARE @CreatedByID BIGINT = 100009
DECLARE @ModifiedDate DATETIME = '5/23/2019 2:36:49 PM'
DECLARE @ModifiedByID BIGINT = 100009
 


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
	INSERT INTO [dbo].[OrderItem]
		(
	 [OrderID],
	 [ImageID],
	 [Width],
	 [Height],
	 [SizeID],
	 [FrameTypeID],
	 [FrameSizeID],
	 [MatID],
	 [MaterialTypeID],
	 [MountingTypeID],
	 [ItemCount],
	 [PriceAmountPerItem],
	 [PriceCurrencyID],
	 [Comments],
	 [PrintingHouseID],
	 [IsDeleted],
	 [CreatedDate],
	 [CreatedByID],
	 [ModifiedDate],
	 [ModifiedByID]
		)
	SELECT 		
			 @OrderID,
	 @ImageID,
	 @Width,
	 @Height,
	 @SizeID,
	 @FrameTypeID,
	 @FrameSizeID,
	 @MatID,
	 @MaterialTypeID,
	 @MountingTypeID,
	 @ItemCount,
	 @PriceAmountPerItem,
	 @PriceCurrencyID,
	 @Comments,
	 @PrintingHouseID,
	 @IsDeleted,
	 @CreatedDate,
	 @CreatedByID,
	 @ModifiedDate,
	 @ModifiedByID
END

SELECT TOP 1 
	@ID = [ID]
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

SELECT 
	@ID
