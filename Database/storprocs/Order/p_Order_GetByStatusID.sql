




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_GetByStatusID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_GetByStatusID]
GO

CREATE PROCEDURE [dbo].[p_Order_GetByStatusID]

	@StatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[StatusID] = @StatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[StatusID] = @StatusID	

	END
	ELSE
		SET @Found = 0;
END
GO