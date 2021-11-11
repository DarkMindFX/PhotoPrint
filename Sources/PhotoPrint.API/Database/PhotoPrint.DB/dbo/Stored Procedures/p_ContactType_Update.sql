
CREATE PROCEDURE [dbo].[p_ContactType_Update]
			@ID BIGINT,
			@ContactTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ContactType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ContactType]
		SET
									[ContactTypeName] = IIF( @ContactTypeName IS NOT NULL, @ContactTypeName, [ContactTypeName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ContactType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ContactType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN e.[ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
