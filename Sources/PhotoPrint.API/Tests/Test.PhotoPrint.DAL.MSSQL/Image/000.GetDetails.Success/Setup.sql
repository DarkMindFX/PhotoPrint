


DECLARE @ID BIGINT = NULL
DECLARE @Title NVARCHAR(50) = 'Title 159cf8b0946e4b02b7d89cc5be42dba4'
DECLARE @Description NVARCHAR(1000) = 'Description 159cf8b0946e4b02b7d89cc5be42dba4'
DECLARE @OriginUrl NVARCHAR(3000) = 'OriginUrl 159cf8b0946e4b02b7d89cc5be42dba4'
DECLARE @MaxWidth INT = 133
DECLARE @MaxHeight INT = 133
DECLARE @PriceAmount DECIMAL(18, 2) = 132394.98
DECLARE @PriceCurrencyID BIGINT = 81
DECLARE @IsDeleted BIT = 1
DECLARE @CreatedByID BIGINT = 100009
DECLARE @CreatedDate DATETIME = '1/3/2024 7:09:39 AM'
DECLARE @ModifiedByID BIGINT = 100005
DECLARE @ModifiedDate DATETIME = '4/7/2019 11:10:39 PM'
 


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
