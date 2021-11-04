


DECLARE @ID BIGINT = NULL
DECLARE @AddressTypeID BIGINT = 6
DECLARE @Title NVARCHAR(50) = 'Title cb4ae7392f994b19beff2e23cc497021'
DECLARE @CityID BIGINT = 4
DECLARE @Street NVARCHAR(50) = 'Street cb4ae7392f994b19beff2e23cc497021'
DECLARE @BuildingNo NVARCHAR(50) = 'BuildingNo cb4ae7392f994b19beff2e23cc497021'
DECLARE @ApartmentNo NVARCHAR(50) = 'ApartmentNo cb4ae7392f994b19beff2e23cc497021'
DECLARE @Comment NVARCHAR(1000) = 'Comment cb4ae7392f994b19beff2e23cc497021'
DECLARE @CreatedByID BIGINT = 100009
DECLARE @CreatedDate DATETIME = '3/10/2020 8:23:38 PM'
DECLARE @ModifiedByID BIGINT = 100002
DECLARE @ModifiedDate DATETIME = '1/19/2023 6:10:38 AM'
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
