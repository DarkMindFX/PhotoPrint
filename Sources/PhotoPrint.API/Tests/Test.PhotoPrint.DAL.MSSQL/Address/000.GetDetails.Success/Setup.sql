

DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeID BIGINT = 6
DECLARE @Title NVARCHAR(50) = 'Title 51afaf29fabb439497450d01f77efac9'
DECLARE @CityID BIGINT = 2
DECLARE @Street NVARCHAR(50) = 'Street 51afaf29fabb439497450d01f77efac9'
DECLARE @BuildingNo NVARCHAR(50) = 'BuildingNo 51afaf29fabb439497450d01f77efac9'
DECLARE @ApartmentNo NVARCHAR(50) = 'ApartmentNo 51afaf29fabb439497450d01f77efac9'
DECLARE @Comment NVARCHAR(1000) = 'Comment 51afaf29fabb439497450d01f77efac9'
DECLARE @CreatedByID BIGINT = 100001
DECLARE @CreatedDate DATETIME = '7/9/2019 5:28:47 AM'
DECLARE @ModifiedByID BIGINT = 100004
DECLARE @ModifiedDate DATETIME = '2/12/2023 7:16:47 AM'
DECLARE @IsDeleted BIT = 0
 


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
