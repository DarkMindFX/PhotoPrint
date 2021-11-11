
CREATE PROCEDURE [dbo].[p_ImageThumbnail_Update]
			@ID BIGINT,
			@Url NVARCHAR(1000),
			@Order INT,
			@ImageID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImageThumbnail]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ImageThumbnail]
		SET
									[Url] = IIF( @Url IS NOT NULL, @Url, [Url] ) ,
									[Order] = IIF( @Order IS NOT NULL, @Order, [Order] ) ,
									[ImageID] = IIF( @ImageID IS NOT NULL, @ImageID, [ImageID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImageThumbnail was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImageThumbnail] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Url IS NOT NULL THEN (CASE WHEN e.[Url] = @Url THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Order IS NOT NULL THEN (CASE WHEN e.[Order] = @Order THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImageID IS NOT NULL THEN (CASE WHEN e.[ImageID] = @ImageID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
