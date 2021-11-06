





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Category_GetByParentID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Category_GetByParentID]
GO

CREATE PROCEDURE [dbo].[p_Category_GetByParentID]

	@ParentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Category] c 
				WHERE
					[ParentID] = @ParentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Category] e
		WHERE 
			[ParentID] = @ParentID	

	END
	ELSE
		SET @Found = 0;
END
GO