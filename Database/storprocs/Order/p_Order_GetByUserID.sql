




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByUserID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByUserID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO