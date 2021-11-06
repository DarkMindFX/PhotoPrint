


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PaymentMethod_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_PaymentMethod_Insert]
GO

CREATE PROCEDURE [dbo].[p_PaymentMethod_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[PaymentMethod]
	SELECT 
		@Name,
		@Description,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[PaymentMethod] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO