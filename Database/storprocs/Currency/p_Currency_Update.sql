

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Currency_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Currency_Update]
GO

CREATE PROCEDURE [dbo].[p_Currency_Update]
			@ID BIGINT,
			@ISO NVARCHAR(5),
			@CurrencyName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Currency]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Currency]
		SET
									[ISO] = IIF( @ISO IS NOT NULL, @ISO, [ISO] ) ,
									[CurrencyName] = IIF( @CurrencyName IS NOT NULL, @CurrencyName, [CurrencyName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Currency was not found', 1;
	END	

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