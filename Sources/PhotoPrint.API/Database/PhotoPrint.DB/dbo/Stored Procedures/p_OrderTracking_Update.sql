
CREATE PROCEDURE [dbo].[p_OrderTracking_Update]
			@ID BIGINT,
			@OrderID BIGINT,
			@OrderStatusID BIGINT,
			@SetDate DATETIME,
			@SetByID BIGINT,
			@Comment NVARCHAR(1000)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderTracking]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OrderTracking]
		SET
									[OrderID] = IIF( @OrderID IS NOT NULL, @OrderID, [OrderID] ) ,
									[OrderStatusID] = IIF( @OrderStatusID IS NOT NULL, @OrderStatusID, [OrderStatusID] ) ,
									[SetDate] = IIF( @SetDate IS NOT NULL, @SetDate, [SetDate] ) ,
									[SetByID] = IIF( @SetByID IS NOT NULL, @SetByID, [SetByID] ) ,
									[Comment] = IIF( @Comment IS NOT NULL, @Comment, [Comment] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderTracking was not found', 1;
	END	

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
