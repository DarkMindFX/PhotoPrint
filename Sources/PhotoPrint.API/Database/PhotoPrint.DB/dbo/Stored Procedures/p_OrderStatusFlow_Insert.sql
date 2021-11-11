
CREATE PROCEDURE [dbo].[p_OrderStatusFlow_Insert]
			@FromStatusID BIGINT,
			@ToStatusID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OrderStatusFlow]
	SELECT 
		@FromStatusID,
		@ToStatusID
	
	

	SELECT
		e.*
	FROM
		[dbo].[OrderStatusFlow] e
	WHERE
				(CASE WHEN @FromStatusID IS NOT NULL THEN (CASE WHEN e.[FromStatusID] = @FromStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ToStatusID IS NOT NULL THEN (CASE WHEN e.[ToStatusID] = @ToStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
