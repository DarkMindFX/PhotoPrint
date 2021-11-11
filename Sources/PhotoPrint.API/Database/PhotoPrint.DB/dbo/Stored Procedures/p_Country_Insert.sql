
CREATE PROCEDURE [dbo].[p_Country_Insert]
			@ID BIGINT,
			@CountryName NVARCHAR(50),
			@ISO NVARCHAR(5),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Country]
	SELECT 
		@CountryName,
		@ISO,
		@IsDeleted
	
	

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
