

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_Insert]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_Insert]
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


	INSERT INTO [dbo].[OrderItem]
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
GO