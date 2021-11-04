


DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeID BIGINT = 3
DECLARE @Title NVARCHAR(50) = 'Title 8f778d3dc7d246078c6fa4ae54331908'
DECLARE @CityID BIGINT = 23
DECLARE @Street NVARCHAR(50) = 'Street 8f778d3dc7d246078c6fa4ae54331908'
DECLARE @BuildingNo NVARCHAR(50) = 'BuildingNo 8f778d3dc7d246078c6fa4ae54331908'
DECLARE @ApartmentNo NVARCHAR(50) = 'ApartmentNo 8f778d3dc7d246078c6fa4ae54331908'
DECLARE @Comment NVARCHAR(1000) = 'Comment 8f778d3dc7d246078c6fa4ae54331908'
DECLARE @CreatedByID BIGINT = 100003
DECLARE @CreatedDate DATETIME = '12/12/2019 10:10:38 AM'
DECLARE @ModifiedByID BIGINT = 100009
DECLARE @ModifiedDate DATETIME = '10/21/2022 10:37:38 AM'
DECLARE @IsDeleted BIT = 1
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[Address]
				WHERE 
	IsDeleted = 0 AND

	1=1 AND
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
	1=1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[Address]
	WHERE 
	1=1 AND
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
	1=1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Address was not deleted', 1
END