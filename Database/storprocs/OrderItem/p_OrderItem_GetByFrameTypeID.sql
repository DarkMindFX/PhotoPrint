





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByFrameTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByFrameTypeID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByFrameTypeID]

	@FrameTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[FrameTypeID] = @FrameTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[FrameTypeID] = @FrameTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO