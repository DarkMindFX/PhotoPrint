
CREATE PROCEDURE [dbo].[p_Image_Update]
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


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Image]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Image]
		SET
									[Title] = IIF( @Title IS NOT NULL, @Title, [Title] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[OriginUrl] = IIF( @OriginUrl IS NOT NULL, @OriginUrl, [OriginUrl] ) ,
									[MaxWidth] = IIF( @MaxWidth IS NOT NULL, @MaxWidth, [MaxWidth] ) ,
									[MaxHeight] = IIF( @MaxHeight IS NOT NULL, @MaxHeight, [MaxHeight] ) ,
									[PriceAmount] = IIF( @PriceAmount IS NOT NULL, @PriceAmount, [PriceAmount] ) ,
									[PriceCurrencyID] = IIF( @PriceCurrencyID IS NOT NULL, @PriceCurrencyID, [PriceCurrencyID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Image was not found', 1;
	END	

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
