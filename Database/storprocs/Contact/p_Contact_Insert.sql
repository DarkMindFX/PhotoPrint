


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Contact_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Contact_Insert]
GO

CREATE PROCEDURE [dbo].[p_Contact_Insert]
			@ID BIGINT,
			@ContactTypeID BIGINT,
			@Title NVARCHAR(50),
			@Comment NVARCHAR(250),
			@Value NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedByID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedByID BIGINT,
			@ModifiedDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Contact]
	SELECT 
		@ContactTypeID,
		@Title,
		@Comment,
		@Value,
		@IsDeleted,
		@CreatedByID,
		@CreatedDate,
		@ModifiedByID,
		@ModifiedDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[Contact] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactTypeID IS NOT NULL THEN (CASE WHEN e.[ContactTypeID] = @ContactTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Title IS NOT NULL THEN (CASE WHEN e.[Title] = @Title THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Value IS NOT NULL THEN (CASE WHEN e.[Value] = @Value THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO