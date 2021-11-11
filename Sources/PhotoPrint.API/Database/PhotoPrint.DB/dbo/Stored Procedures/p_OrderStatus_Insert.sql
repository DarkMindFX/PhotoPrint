
CREATE PROCEDURE [dbo].[p_OrderStatus_Insert]
			@ID BIGINT,
			@OrderStatusName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderStatus]
	SELECT 
		@OrderStatusName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN e.[OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
