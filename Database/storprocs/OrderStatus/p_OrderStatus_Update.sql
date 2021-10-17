

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatus_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatus_Update]
GO

CREATE PROCEDURE [dbo].[p_OrderStatus_Update]
			@ID BIGINT,
			@OrderStatusName NVARCHAR(50),
			@IsDeleted BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatus]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OrderStatus]
		SET
									[OrderStatusName] = IIF( @OrderStatusName IS NOT NULL, @OrderStatusName, [OrderStatusName] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderStatus was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OrderStatus] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OrderStatusName IS NOT NULL THEN (CASE WHEN e.[OrderStatusName] = @OrderStatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO