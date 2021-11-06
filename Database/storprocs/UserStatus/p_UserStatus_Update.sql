


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserStatus_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserStatus_Update]
GO

CREATE PROCEDURE [dbo].[p_UserStatus_Update]
			@ID BIGINT,
			@StatusName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserStatus]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[UserStatus]
		SET
									[StatusName] = IIF( @StatusName IS NOT NULL, @StatusName, [StatusName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserStatus was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN e.[StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO