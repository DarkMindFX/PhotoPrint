

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_Insert]
GO

CREATE PROCEDURE [dbo].[p_Region_Insert]
			@ID BIGINT,
			@RegionName NVARCHAR(50),
			@CountryID BIGINT,
			@IsDeleted BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Region]
	SELECT 
		@RegionName,
		@CountryID,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[Region] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RegionName IS NOT NULL THEN (CASE WHEN e.[RegionName] = @RegionName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CountryID IS NOT NULL THEN (CASE WHEN e.[CountryID] = @CountryID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO