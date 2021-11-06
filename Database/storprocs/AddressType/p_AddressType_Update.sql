


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_Update]
GO

CREATE PROCEDURE [dbo].[p_AddressType_Update]
			@ID BIGINT,
			@AddressTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[AddressType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[AddressType]
		SET
									[AddressTypeName] = IIF( @AddressTypeName IS NOT NULL, @AddressTypeName, [AddressTypeName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'AddressType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[AddressType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressTypeName IS NOT NULL THEN (CASE WHEN e.[AddressTypeName] = @AddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO