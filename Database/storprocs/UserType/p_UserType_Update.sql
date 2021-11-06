


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserType_Update]
GO

CREATE PROCEDURE [dbo].[p_UserType_Update]
			@ID BIGINT,
			@UserTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[UserType]
		SET
									[UserTypeName] = IIF( @UserTypeName IS NOT NULL, @UserTypeName, [UserTypeName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserTypeName IS NOT NULL THEN (CASE WHEN e.[UserTypeName] = @UserTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO