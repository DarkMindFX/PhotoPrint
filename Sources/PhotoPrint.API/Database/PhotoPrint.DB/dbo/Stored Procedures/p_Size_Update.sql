
CREATE PROCEDURE [dbo].[p_Size_Update]
			@ID BIGINT,
			@SizeName NVARCHAR(50),
			@Width INT,
			@Height INT,
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Size]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Size]
		SET
									[SizeName] = IIF( @SizeName IS NOT NULL, @SizeName, [SizeName] ) ,
									[Width] = IIF( @Width IS NOT NULL, @Width, [Width] ) ,
									[Height] = IIF( @Height IS NOT NULL, @Height, [Height] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Size was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Size] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SizeName IS NOT NULL THEN (CASE WHEN e.[SizeName] = @SizeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Width IS NOT NULL THEN (CASE WHEN e.[Width] = @Width THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Height IS NOT NULL THEN (CASE WHEN e.[Height] = @Height THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
