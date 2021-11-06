


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_Update]
GO

CREATE PROCEDURE [dbo].[p_Unit_Update]
			@ID BIGINT,
			@UnitName NVARCHAR(50),
			@UnitAbbr NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Unit]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Unit]
		SET
									[UnitName] = IIF( @UnitName IS NOT NULL, @UnitName, [UnitName] ) ,
									[UnitAbbr] = IIF( @UnitAbbr IS NOT NULL, @UnitAbbr, [UnitAbbr] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Unit was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Unit] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnitName IS NOT NULL THEN (CASE WHEN e.[UnitName] = @UnitName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnitAbbr IS NOT NULL THEN (CASE WHEN e.[UnitAbbr] = @UnitAbbr THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO