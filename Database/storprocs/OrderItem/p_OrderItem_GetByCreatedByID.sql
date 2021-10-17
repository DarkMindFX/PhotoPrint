




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderItem_GetByCreatedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderItem_GetByCreatedByID]
GO

CREATE PROCEDURE [dbo].[p_OrderItem_GetByCreatedByID]

	@CreatedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[CreatedByID] = @CreatedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[CreatedByID] = @CreatedByID	

	END
	ELSE
		SET @Found = 0;
END
GO