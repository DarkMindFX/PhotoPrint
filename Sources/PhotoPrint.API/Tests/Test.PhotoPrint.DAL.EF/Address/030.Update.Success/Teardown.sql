


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeID BIGINT = 5
DECLARE @Title NVARCHAR(50) = 'Title aa9c8cbcf4584ea1832bfd6ba06e9324'
DECLARE @CityID BIGINT = 2
DECLARE @Street NVARCHAR(50) = 'Street aa9c8cbcf4584ea1832bfd6ba06e9324'
DECLARE @BuildingNo NVARCHAR(50) = 'BuildingNo aa9c8cbcf4584ea1832bfd6ba06e9324'
DECLARE @ApartmentNo NVARCHAR(50) = 'ApartmentNo aa9c8cbcf4584ea1832bfd6ba06e9324'
DECLARE @Comment NVARCHAR(1000) = 'Comment aa9c8cbcf4584ea1832bfd6ba06e9324'
DECLARE @CreatedByID BIGINT = 100010
DECLARE @CreatedDate DATETIME = '9/6/2020 2:11:38 AM'
DECLARE @ModifiedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '9/6/2020 2:11:38 AM'
DECLARE @IsDeleted BIT = 1
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updAddressTypeID BIGINT = 5
DECLARE @updTitle NVARCHAR(50) = 'Title a83d33ef6a2142b7aa70cee52c4794c3'
DECLARE @updCityID BIGINT = 7
DECLARE @updStreet NVARCHAR(50) = 'Street a83d33ef6a2142b7aa70cee52c4794c3'
DECLARE @updBuildingNo NVARCHAR(50) = 'BuildingNo a83d33ef6a2142b7aa70cee52c4794c3'
DECLARE @updApartmentNo NVARCHAR(50) = 'ApartmentNo a83d33ef6a2142b7aa70cee52c4794c3'
DECLARE @updComment NVARCHAR(1000) = 'Comment a83d33ef6a2142b7aa70cee52c4794c3'
DECLARE @updCreatedByID BIGINT = 100006
DECLARE @updCreatedDate DATETIME = '7/18/2023 11:58:38 AM'
DECLARE @updModifiedByID BIGINT = 100007
DECLARE @updModifiedDate DATETIME = '12/4/2020 12:24:38 PM'
DECLARE @updIsDeleted BIT = 1
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Address]
				WHERE 
	(CASE WHEN @updAddressTypeID IS NOT NULL THEN (CASE WHEN [AddressTypeID] = @updAddressTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCityID IS NOT NULL THEN (CASE WHEN [CityID] = @updCityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updStreet IS NOT NULL THEN (CASE WHEN [Street] = @updStreet THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updBuildingNo IS NOT NULL THEN (CASE WHEN [BuildingNo] = @updBuildingNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updApartmentNo IS NOT NULL THEN (CASE WHEN [ApartmentNo] = @updApartmentNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updComment IS NOT NULL THEN (CASE WHEN [Comment] = @updComment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Address]
	WHERE 
	(CASE WHEN @AddressTypeID IS NOT NULL THEN (CASE WHEN [AddressTypeID] = @AddressTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN [Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN [CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Street IS NOT NULL THEN (CASE WHEN [Street] = @Street THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @BuildingNo IS NOT NULL THEN (CASE WHEN [BuildingNo] = @BuildingNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ApartmentNo IS NOT NULL THEN (CASE WHEN [ApartmentNo] = @ApartmentNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN [Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Address]
	WHERE 
	(CASE WHEN @updAddressTypeID IS NOT NULL THEN (CASE WHEN [AddressTypeID] = @updAddressTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitle IS NOT NULL THEN (CASE WHEN [Title] = @updTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCityID IS NOT NULL THEN (CASE WHEN [CityID] = @updCityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updStreet IS NOT NULL THEN (CASE WHEN [Street] = @updStreet THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updBuildingNo IS NOT NULL THEN (CASE WHEN [BuildingNo] = @updBuildingNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updApartmentNo IS NOT NULL THEN (CASE WHEN [ApartmentNo] = @updApartmentNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updComment IS NOT NULL THEN (CASE WHEN [Comment] = @updComment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedByID IS NOT NULL THEN (CASE WHEN [CreatedByID] = @updCreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @updIsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Address was not updated', 1
END