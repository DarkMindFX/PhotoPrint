

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Image_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Image_Insert]
GO

CREATE PROCEDURE [dbo].[p_Image_Insert]
			@ID BIGINT,
			@Title NVARCHAR(50),
			@Description NVARCHAR(1000),
			@OriginUrl NVARCHAR(3000),
			@MaxWidth INT,
			@MaxHeight INT,
			@PriceAmount DECIMAL(18, 2),
			@PriceCurrencyID BIGINT,
			@IsDeleted BIT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Image]
	SELECT 
		@Title,
		@Description,
		@OriginUrl,
		@MaxWidth,
		@MaxHeight,
		@PriceAmount,
		@PriceCurrencyID,
		@IsDeleted,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Image] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OriginUrl IS NOT NULL THEN (CASE WHEN e.[OriginUrl] = @OriginUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaxWidth IS NOT NULL THEN (CASE WHEN e.[MaxWidth] = @MaxWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MaxHeight IS NOT NULL THEN (CASE WHEN e.[MaxHeight] = @MaxHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceAmount IS NOT NULL THEN (CASE WHEN e.[PriceAmount] = @PriceAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN e.[PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO