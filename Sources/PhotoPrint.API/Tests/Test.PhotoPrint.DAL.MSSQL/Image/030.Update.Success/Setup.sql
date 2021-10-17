

DECLARE @ID BIGINT = NULL
DECLARE @Title NVARCHAR(50) = 'Title 8a4fce99b43e4df586c7ccce803cdb88'
DECLARE @Description NVARCHAR(1000) = 'Description 8a4fce99b43e4df586c7ccce803cdb88'
DECLARE @OriginUrl NVARCHAR(3000) = 'OriginUrl 8a4fce99b43e4df586c7ccce803cdb88'
DECLARE @MaxWidth INT = 675
DECLARE @MaxHeight INT = 675
DECLARE @PriceAmount DECIMAL(18, 2) = 128
DECLARE @PriceCurrencyID BIGINT = 173
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedByID BIGINT = 100008
DECLARE @CreatedDate DATETIME = '12/31/2022 11:54:48 AM'
DECLARE @ModifiedByID BIGINT = 100007
DECLARE @ModifiedDate DATETIME = '12/31/2022 11:54:48 AM'
 


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
