

DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeID BIGINT = 2
DECLARE @Title NVARCHAR(50) = 'Title dc3480a1231d484491e1afb6406bbf69'
DECLARE @CityID BIGINT = 10
DECLARE @Street NVARCHAR(50) = 'Street dc3480a1231d484491e1afb6406bbf69'
DECLARE @BuildingNo NVARCHAR(50) = 'BuildingNo dc3480a1231d484491e1afb6406bbf69'
DECLARE @ApartmentNo NVARCHAR(50) = 'ApartmentNo dc3480a1231d484491e1afb6406bbf69'
DECLARE @Comment NVARCHAR(1000) = 'Comment dc3480a1231d484491e1afb6406bbf69'
DECLARE @CreatedByID BIGINT = 100011
DECLARE @CreatedDate DATETIME = '11/22/2022 10:22:32 PM'
DECLARE @ModifiedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '11/22/2022 10:22:32 PM'
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
