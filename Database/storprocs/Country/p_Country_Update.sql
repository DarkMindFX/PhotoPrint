

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Country_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Country_Update]
GO

CREATE PROCEDURE [dbo].[p_Country_Update]
			@ID BIGINT,
			@CountryName NVARCHAR(50),
			@ISO NVARCHAR(5),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Country]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Country]
		SET
									[CountryName] = IIF( @CountryName IS NOT NULL, @CountryName, [CountryName] ) ,
									[ISO] = IIF( @ISO IS NOT NULL, @ISO, [ISO] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Country was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Country] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CountryName IS NOT NULL THEN (CASE WHEN e.[CountryName] = @CountryName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ISO IS NOT NULL THEN (CASE WHEN e.[ISO] = @ISO THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO