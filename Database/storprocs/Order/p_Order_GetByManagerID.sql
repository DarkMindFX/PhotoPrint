




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByManagerID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByManagerID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByManagerID]

	@ManagerID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[ManagerID] = @ManagerID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[ManagerID] = @ManagerID	

	END
	ELSE
		SET @Found = 0;
END
GO