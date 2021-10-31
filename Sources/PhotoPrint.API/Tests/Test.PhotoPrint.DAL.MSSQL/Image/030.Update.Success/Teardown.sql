

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Title NVARCHAR(50) = 'Title 233c563b4abc4c6d8caaa04095276a98'
DECLARE @Description NVARCHAR(1000) = 'Description 233c563b4abc4c6d8caaa04095276a98'
DECLARE @OriginUrl NVARCHAR(3000) = 'OriginUrl 233c563b4abc4c6d8caaa04095276a98'
DECLARE @MaxWidth INT = 879
DECLARE @MaxHeight INT = 879
DECLARE @PriceAmount DECIMAL(18, 2) = 879073.43
DECLARE @PriceCurrencyID BIGINT = 139
DECLARE @IsDeleted BIT = 0
DECLARE @CreatedByID BIGINT = 100009
DECLARE @CreatedDate DATETIME = '5/25/2024 10:09:33 AM'
DECLARE @ModifiedByID BIGINT = 100010
DECLARE @ModifiedDate DATETIME = '5/25/2024 10:09:33 AM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updTitle NVARCHAR(50) = 'Title bc07aeca0c44413b99ba1a3af5af3e04'
DECLARE @updDescription NVARCHAR(1000) = 'Description bc07aeca0c44413b99ba1a3af5af3e04'
DECLARE @updOriginUrl NVARCHAR(3000) = 'OriginUrl bc07aeca0c44413b99ba1a3af5af3e04'
DECLARE @updMaxWidth INT = 968
DECLARE @updMaxHeight INT = 968
DECLARE @updPriceAmount DECIMAL(18, 2) = 968774.69
DECLARE @updPriceCurrencyID BIGINT = 151
DECLARE @updIsDeleted BIT = 1
DECLARE @updCreatedByID BIGINT = 100011
DECLARE @updCreatedDate DATETIME = '3/4/2019 5:43:33 AM'
DECLARE @updModifiedByID BIGINT = 100001
DECLARE @updModifiedDate DATETIME = '3/4/2019 5:43:33 AM'
 

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