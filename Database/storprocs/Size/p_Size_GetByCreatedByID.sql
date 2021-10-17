




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Size_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Size_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_Size_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Size] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Size] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO