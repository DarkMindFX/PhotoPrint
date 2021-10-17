

-- original values --
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
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updTitle NVARCHAR(50) = 'Title 155bb1a73abf471198abdfa4781e190f'
DECLARE @updDescription NVARCHAR(1000) = 'Description 155bb1a73abf471198abdfa4781e190f'
DECLARE @updOriginUrl NVARCHAR(3000) = 'OriginUrl 155bb1a73abf471198abdfa4781e190f'
DECLARE @updMaxWidth INT = 719
DECLARE @updMaxHeight INT = 719
DECLARE @updPriceAmount DECIMAL(18, 2) = 129
DECLARE @updPriceCurrencyID BIGINT = 203
DECLARE @updIsDeleted BIT = 0
DECLARE @updCreatedByID BIGINT = 100007
DECLARE @updCreatedDate DATETIME = '5/19/2020 12:20:48 PM'
DECLARE @updModifiedByID BIGINT = 100003
DECLARE @updModifiedDate DATETIME = '5/19/2020 12:20:48 PM'
 

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