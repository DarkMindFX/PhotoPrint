


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ContactType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ContactType_Insert]
GO

CREATE PROCEDURE [dbo].[p_ContactType_Insert]
			@ID BIGINT,
			@ContactTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ContactType]
	SELECT 
		@ContactTypeName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[ContactType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactTypeName IS NOT NULL THEN (CASE WHEN e.[ContactTypeName] = @ContactTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO