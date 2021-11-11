
CREATE PROCEDURE [dbo].[p_PaymentMethod_Update]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PaymentMethod]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[PaymentMethod]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PaymentMethod was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PaymentMethod] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
