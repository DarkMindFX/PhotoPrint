


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderTracking_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderTracking_Insert]
GO

CREATE PROCEDURE [dbo].[p_OrderTracking_Insert]
			@ID BIGINT,
			@OrderID BIGINT,
			@OrderStatusID BIGINT,
			@SetDate DATETIME,
			@SetByID BIGINT,
			@Comment NVARCHAR(1000)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderTracking]
	SELECT 
		@OrderID,
		@OrderStatusID,
		@SetDate,
		@SetByID,
		@Comment
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderTracking] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderID IS NOT NULL THEN (CASE WHEN e.[OrderID] = @OrderID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderStatusID IS NOT NULL THEN (CASE WHEN e.[OrderStatusID] = @OrderStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SetDate IS NOT NULL THEN (CASE WHEN e.[SetDate] = @SetDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SetByID IS NOT NULL THEN (CASE WHEN e.[SetByID] = @SetByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comment IS NOT NULL THEN (CASE WHEN e.[Comment] = @Comment THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO