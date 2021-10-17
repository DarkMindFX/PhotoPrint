

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Unit_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Unit_Insert]
GO

CREATE PROCEDURE [dbo].[p_Unit_Insert]
			@ID BIGINT,
			@UnitName NVARCHAR(50),
			@UnitAbbr NVARCHAR(50),
			@Description NVARCHAR(1000),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Unit]
	SELECT 
		@UnitName,
		@UnitAbbr,
		@Description,
		@IsDeleted
	
	

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