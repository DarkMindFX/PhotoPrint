


DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeID BIGINT = 2
DECLARE @Title NVARCHAR(50) = 'Title db5a2f197cad4748bd1b94850f713ddf'
DECLARE @CityID BIGINT = 1
DECLARE @Street NVARCHAR(50) = 'Street db5a2f197cad4748bd1b94850f713ddf'
DECLARE @BuildingNo NVARCHAR(50) = 'BuildingNo db5a2f197cad4748bd1b94850f713ddf'
DECLARE @ApartmentNo NVARCHAR(50) = 'ApartmentNo db5a2f197cad4748bd1b94850f713ddf'
DECLARE @Comment NVARCHAR(1000) = 'Comment db5a2f197cad4748bd1b94850f713ddf'
DECLARE @CreatedByID BIGINT = 100008
DECLARE @CreatedDate DATETIME = '10/28/2021 8:22:38 AM'
DECLARE @ModifiedByID BIGINT = 100008
DECLARE @ModifiedDate DATETIME = '6/15/2019 4:22:38 AM'
DECLARE @IsDeleted BIT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
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
 ))
					
BEGIN
	INSERT INTO [dbo].[Address]
		(
	 [AddressTypeID],
	 [Title],
	 [CityID],
	 [Street],
	 [BuildingNo],
	 [ApartmentNo],
	 [Comment],
	 [CreatedByID],
	 [CreatedDate],
	 [ModifiedByID],
	 [ModifiedDate],
	 [IsDeleted]
		)
	SELECT 		
			 @AddressTypeID,
	 @Title,
	 @CityID,
	 @Street,
	 @BuildingNo,
	 @ApartmentNo,
	 @Comment,
	 @CreatedByID,
	 @CreatedDate,
	 @ModifiedByID,
	 @ModifiedDate,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Address] e
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

SELECT 
	@ID
