


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_Insert]
GO

CREATE PROCEDURE [dbo].[p_Currency_Insert]
			@ID BIGINT,
			@ISO NVARCHAR(5),
			@CurrencyName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Currency]
	SELECT 
		@ISO,
		@CurrencyName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[Currency] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN e.[ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CurrencyName IS NOT NULL THEN (CASE WHEN e.[CurrencyName] = @CurrencyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO