




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_GetByFromStatusID', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_GetByFromStatusID]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_GetByFromStatusID]

	@FromStatusID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatusFlow] c 
				WHERE
					[FromStatusID] = @FromStatusID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderStatusFlow] e
		WHERE 
			[FromStatusID] = @FromStatusID	

	END
	ELSE
		SET @Found = 0;
END
GO