


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OrderStatusFlow_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OrderStatusFlow_Update]
GO

CREATE PROCEDURE [dbo].[p_OrderStatusFlow_Update]
			@FromStatusID BIGINT,
			@ToStatusID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderStatusFlow]
					WHERE 
												[FromStatusID] = @FromStatusID	AND
												[ToStatusID] = @ToStatusID	
							))
	BEGIN
		UPDATE [dbo].[OrderStatusFlow]
		SET
									[FromStatusID] = IIF( @FromStatusID IS NOT NULL, @FromStatusID, [FromStatusID] ) ,
									[ToStatusID] = IIF( @ToStatusID IS NOT NULL, @ToStatusID, [ToStatusID] ) 
						WHERE 
												[FromStatusID] = @FromStatusID	AND
												[ToStatusID] = @ToStatusID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OrderStatusFlow was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OrderStatusFlow] e
	WHERE
				(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN e.[FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN e.[ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO