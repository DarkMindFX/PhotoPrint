





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_FrameType_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_FrameType_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_FrameType_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[FrameType] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[FrameType] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO