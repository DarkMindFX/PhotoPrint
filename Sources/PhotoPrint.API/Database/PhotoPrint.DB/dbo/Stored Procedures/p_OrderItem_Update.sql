
CREATE PROCEDURE [dbo].[p_OrderItem_Update]
			@ID BIGINT,
			@OrderID BIGINT,
			@ImageID BIGINT,
			@Width INT,
			@Height INT,
			@SizeID BIGINT,
			@FrameTypeID BIGINT,
			@FrameSizeID BIGINT,
			@MatID BIGINT,
			@MaterialTypeID BIGINT,
			@MountingTypeID BIGINT,
			@ItemCount INT,
			@PriceAmountPerItem DECIMAL(18, 2),
			@PriceCurrencyID BIGINT,
			@Comments NVARCHAR(1000),
			@PrintingHouseID BIGINT,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OrderItem]
		SET
									[OrderID] = IIF( @OrderID IS NOT NULL, @OrderID, [OrderID] ) ,
									[ImageID] = IIF( @ImageID IS NOT NULL, @ImageID, [ImageID] ) ,
									[Width] = IIF( @Width IS NOT NULL, @Width, [Width] ) ,
									[Height] = IIF( @Height IS NOT NULL, @Height, [Height] ) ,
									[SizeID] = IIF( @SizeID IS NOT NULL, @SizeID, [SizeID] ) ,
									[FrameTypeID] = IIF( @FrameTypeID IS NOT NULL, @FrameTypeID, [FrameTypeID] ) ,
									[FrameSizeID] = IIF( @FrameSizeID IS NOT NULL, @FrameSizeID, [FrameSizeID] ) ,
									[MatID] = IIF( @MatID IS NOT NULL, @MatID, [MatID] ) ,
									[MaterialTypeID] = IIF( @MaterialTypeID IS NOT NULL, @MaterialTypeID, [MaterialTypeID] ) ,
									[MountingTypeID] = IIF( @MountingTypeID IS NOT NULL, @MountingTypeID, [MountingTypeID] ) ,
									[ItemCount] = IIF( @ItemCount IS NOT NULL, @ItemCount, [ItemCount] ) ,
									[PriceAmountPerItem] = IIF( @PriceAmountPerItem IS NOT NULL, @PriceAmountPerItem, [PriceAmountPerItem] ) ,
									[PriceCurrencyID] = IIF( @PriceCurrencyID IS NOT NULL, @PriceCurrencyID, [PriceCurrencyID] ) ,
									[Comments] = IIF( @Comments IS NOT NULL, @Comments, [Comments] ) ,
									[PrintingHouseID] = IIF( @PrintingHouseID IS NOT NULL, @PrintingHouseID, [PrintingHouseID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderItem was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OrderItem] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN e.[Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN e.[Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SizeID IS NOT NULL THEN (CASE WHEN e.[SizeID] = @SizeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameTypeID IS NOT NULL THEN (CASE WHEN e.[FrameTypeID] = @FrameTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameSizeID IS NOT NULL THEN (CASE WHEN e.[FrameSizeID] = @FrameSizeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MatID IS NOT NULL THEN (CASE WHEN e.[MatID] = @MatID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaterialTypeID IS NOT NULL THEN (CASE WHEN e.[MaterialTypeID] = @MaterialTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MountingTypeID IS NOT NULL THEN (CASE WHEN e.[MountingTypeID] = @MountingTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ItemCount IS NOT NULL THEN (CASE WHEN e.[ItemCount] = @ItemCount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceAmountPerItem IS NOT NULL THEN (CASE WHEN e.[PriceAmountPerItem] = @PriceAmountPerItem THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN e.[PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN e.[Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
