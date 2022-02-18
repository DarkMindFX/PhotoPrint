


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserInteriorThumbnail_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserInteriorThumbnail_Update]
GO

CREATE PROCEDURE [dbo].[p_UserInteriorThumbnail_Update]
			@ID BIGINT,
			@UserID BIGINT,
			@Url NVARCHAR(1000)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[UserInteriorThumbnail]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[UserInteriorThumbnail]
		SET
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[Url] = IIF( @Url IS NOT NULL, @Url, [Url] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'UserInteriorThumbnail was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[UserInteriorThumbnail] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN e.[Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO