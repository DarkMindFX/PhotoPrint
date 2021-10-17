

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_Update]
GO

CREATE PROCEDURE [dbo].[p_FrameType_Update]
			@ID BIGINT,
			@FrameTypeName NVARCHAR(50),
			@Description NVARCHAR(1000),
			@ThumbnailUrl NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[FrameType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[FrameType]
		SET
									[FrameTypeName] = IIF( @FrameTypeName IS NOT NULL, @FrameTypeName, [FrameTypeName] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[ThumbnailUrl] = IIF( @ThumbnailUrl IS NOT NULL, @ThumbnailUrl, [ThumbnailUrl] ) ,
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
		THROW 51001, 'FrameType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[FrameType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FrameTypeName IS NOT NULL THEN (CASE WHEN e.[FrameTypeName] = @FrameTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ThumbnailUrl IS NOT NULL THEN (CASE WHEN e.[ThumbnailUrl] = @ThumbnailUrl THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO