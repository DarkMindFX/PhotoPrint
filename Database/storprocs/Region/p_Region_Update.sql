


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Region_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Region_Update]
GO

CREATE PROCEDURE [dbo].[p_Region_Update]
			@ID BIGINT,
			@RegionName NVARCHAR(50),
			@CountryID BIGINT,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Region]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Region]
		SET
									[RegionName] = IIF( @RegionName IS NOT NULL, @RegionName, [RegionName] ) ,
									[CountryID] = IIF( @CountryID IS NOT NULL, @CountryID, [CountryID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Region was not found', 1;
	END	

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