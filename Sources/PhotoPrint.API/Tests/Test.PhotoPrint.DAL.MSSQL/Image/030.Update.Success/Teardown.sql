


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Title NVARCHAR(50) = 'Title 7450baf60fcc4b0c8ffcd0e6d00f40cc'
DECLARE @Description NVARCHAR(1000) = 'Description 7450baf60fcc4b0c8ffcd0e6d00f40cc'
DECLARE @OriginUrl NVARCHAR(3000) = 'OriginUrl 7450baf60fcc4b0c8ffcd0e6d00f40cc'
DECLARE @MaxWidth INT = 404
DECLARE @MaxHeight INT = 404
DECLARE @PriceAmount DECIMAL(18, 2) = 403994.8
DECLARE @PriceCurrencyID BIGINT = 147
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedByID BIGINT = 100009
DECLARE @CreatedDate DATETIME = '3/5/2024 2:23:39 PM'
DECLARE @ModifiedByID BIGINT = 100008
DECLARE @ModifiedDate DATETIME = '7/25/2021 12:10:39 AM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updTitle NVARCHAR(50) = 'Title c802267d455d408995722f74a0f7f160'
DECLARE @updDescription NVARCHAR(1000) = 'Description c802267d455d408995722f74a0f7f160'
DECLARE @updOriginUrl NVARCHAR(3000) = 'OriginUrl c802267d455d408995722f74a0f7f160'
DECLARE @updMaxWidth INT = 449
DECLARE @updMaxHeight INT = 449
DECLARE @updPriceAmount DECIMAL(18, 2) = 448845.43
DECLARE @updPriceCurrencyID BIGINT = 97
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedByID BIGINT = 100001
DECLARE @updCreatedDate DATETIME = '6/4/2024 9:57:39 AM'
DECLARE @updModifiedByID BIGINT = 100004
DECLARE @updModifiedDate DATETIME = '6/4/2024 9:57:39 AM'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Image]
				WHERE 
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOriginUrl IS NOT NULL THEN (CASE WHEN [OriginUrl] = @updOriginUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updMaxWidth IS NOT NULL THEN (CASE WHEN [MaxWidth] = @updMaxWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updMaxHeight IS NOT NULL THEN (CASE WHEN [MaxHeight] = @updMaxHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPriceAmount IS NOT NULL THEN (CASE WHEN [PriceAmount] = @updPriceAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPriceCurrencyID IS NOT NULL THEN (CASE WHEN [PriceCurrencyID] = @updPriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
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

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Image]
	WHERE 
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOriginUrl IS NOT NULL THEN (CASE WHEN [OriginUrl] = @updOriginUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updMaxWidth IS NOT NULL THEN (CASE WHEN [MaxWidth] = @updMaxWidth THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updMaxHeight IS NOT NULL THEN (CASE WHEN [MaxHeight] = @updMaxHeight THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPriceAmount IS NOT NULL THEN (CASE WHEN [PriceAmount] = @updPriceAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPriceCurrencyID IS NOT NULL THEN (CASE WHEN [PriceCurrencyID] = @updPriceCurrencyID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Image was not updated', 1
END