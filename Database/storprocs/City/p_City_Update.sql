

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_City_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_City_Update]
GO

CREATE PROCEDURE [dbo].[p_City_Update]
			@ID BIGINT,
			@CityName NVARCHAR(250),
			@RegionID BIGINT,
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[City]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[City]
		SET
									[CityName] = IIF( @CityName IS NOT NULL, @CityName, [CityName] ) ,
									[RegionID] = IIF( @RegionID IS NOT NULL, @RegionID, [RegionID] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'City was not found', 1;
	END	

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