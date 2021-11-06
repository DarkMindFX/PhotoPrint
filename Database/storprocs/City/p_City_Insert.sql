


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_Insert]
GO

CREATE PROCEDURE [dbo].[p_City_Insert]
			@ID BIGINT,
			@CityName NVARCHAR(250),
			@RegionID BIGINT,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[City]
	SELECT 
		@CityName,
		@RegionID,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[City] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CityName IS NOT NULL THEN (CASE WHEN e.[CityName] = @CityName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RegionID IS NOT NULL THEN (CASE WHEN e.[RegionID] = @RegionID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO