

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_Insert]
GO

CREATE PROCEDURE [dbo].[p_Size_Insert]
			@ID BIGINT,
			@SizeName NVARCHAR(50),
			@Width INT,
			@Height INT,
			@IsDeleted BIGINT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Size]
	SELECT 
		@SizeName,
		@Width,
		@Height,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

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
GO