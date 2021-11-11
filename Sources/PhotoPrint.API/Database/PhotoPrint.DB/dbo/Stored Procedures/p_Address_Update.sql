
CREATE PROCEDURE [dbo].[p_Address_Update]
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


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Address]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Address]
		SET
									[AddressTypeID] = IIF( @AddressTypeID IS NOT NULL, @AddressTypeID, [AddressTypeID] ) ,
									[Title] = IIF( @Title IS NOT NULL, @Title, [Title] ) ,
									[CityID] = IIF( @CityID IS NOT NULL, @CityID, [CityID] ) ,
									[Street] = IIF( @Street IS NOT NULL, @Street, [Street] ) ,
									[BuildingNo] = IIF( @BuildingNo IS NOT NULL, @BuildingNo, [BuildingNo] ) ,
									[ApartmentNo] = IIF( @ApartmentNo IS NOT NULL, @ApartmentNo, [ApartmentNo] ) ,
									[Comment] = IIF( @Comment IS NOT NULL, @Comment, [Comment] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Address was not found', 1;
	END	

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
