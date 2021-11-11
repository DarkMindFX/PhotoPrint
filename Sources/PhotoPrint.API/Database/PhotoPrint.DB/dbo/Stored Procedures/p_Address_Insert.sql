
CREATE PROCEDURE [dbo].[p_Address_Insert]
			@ID BIGINT,
			@AddressTypeID BIGINT,
			@Title NVARCHAR(50),
			@CityID BIGINT,
			@Street NVARCHAR(50),
			@BuildingNo NVARCHAR(50),
			@ApartmentNo NVARCHAR(50),
			@Comment NVARCHAR(1000),
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Address]
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
	
	

	SELECT
		e.*
	FROM
		[dbo].[Address] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressTypeID IS NOT NULL THEN (CASE WHEN e.[AddressTypeID] = @AddressTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityID IS NOT NULL THEN (CASE WHEN e.[CityID] = @CityID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Street IS NOT NULL THEN (CASE WHEN e.[Street] = @Street THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @BuildingNo IS NOT NULL THEN (CASE WHEN e.[BuildingNo] = @BuildingNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ApartmentNo IS NOT NULL THEN (CASE WHEN e.[ApartmentNo] = @ApartmentNo THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
