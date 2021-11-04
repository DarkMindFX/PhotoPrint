


DECLARE @ID BIGINT = NULL
DECLARE @Title NVARCHAR(50) = 'Title e3a7e046d16b4db0820f76a366655fc8'
DECLARE @Description NVARCHAR(1000) = 'Description e3a7e046d16b4db0820f76a366655fc8'
DECLARE @OriginUrl NVARCHAR(3000) = 'OriginUrl e3a7e046d16b4db0820f76a366655fc8'
DECLARE @MaxWidth INT = 75
DECLARE @MaxHeight INT = 75
DECLARE @PriceAmount DECIMAL(18, 2) = 74258.17
DECLARE @PriceCurrencyID BIGINT = 223
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedByID BIGINT = 100009
DECLARE @CreatedDate DATETIME = '12/13/2022 4:35:39 PM'
DECLARE @ModifiedByID BIGINT = 100001
DECLARE @ModifiedDate DATETIME = '5/3/2020 2:22:39 AM'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Image]
				WHERE 
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN [Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OriginUrl IS NOT NULL THEN (CASE WHEN [OriginUrl] = @OriginUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MaxWidth IS NOT NULL THEN (CASE WHEN [MaxWidth] = @MaxWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MaxHeight IS NOT NULL THEN (CASE WHEN [MaxHeight] = @MaxHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PriceAmount IS NOT NULL THEN (CASE WHEN [PriceAmount] = @PriceAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN [PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Image]
		(
	 [Title],
	 [Description],
	 [OriginUrl],
	 [MaxWidth],
	 [MaxHeight],
	 [PriceAmount],
	 [PriceCurrencyID],
	 [IsDeleted],
	 [CreatedByID],
	 [CreatedDate],
	 [ModifiedByID],
	 [ModifiedDate]
		)
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
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Image] e
WHERE
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN [Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OriginUrl IS NOT NULL THEN (CASE WHEN [OriginUrl] = @OriginUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MaxWidth IS NOT NULL THEN (CASE WHEN [MaxWidth] = @MaxWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MaxHeight IS NOT NULL THEN (CASE WHEN [MaxHeight] = @MaxHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PriceAmount IS NOT NULL THEN (CASE WHEN [PriceAmount] = @PriceAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PriceCurrencyID IS NOT NULL THEN (CASE WHEN [PriceCurrencyID] = @PriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
