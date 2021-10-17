

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_Insert]
GO

CREATE PROCEDURE [dbo].[p_Order_Insert]
			@ID BIGINT,
			@ManagerID BIGINT,
			@UserID BIGINT,
			@ContactID BIGINT,
			@DeliveryAddressID BIGINT,
			@DeliveryServiceID BIGINT,
			@Comments NVARCHAR(1000),
			@IsDeleted BIT,
			@CreatedDate DATETIME,
			@CreatedByID BIGINT,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Order]
	SELECT 
		@ManagerID,
		@UserID,
		@ContactID,
		@DeliveryAddressID,
		@DeliveryServiceID,
		@Comments,
		@IsDeleted,
		@CreatedDate,
		@CreatedByID,
		@ModifiedDate,
		@ModifiedByID
	
	

	SELECT
		e.*
	FROM
		[dbo].[Order] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ManagerID IS NOT NULL THEN (CASE WHEN e.[ManagerID] = @ManagerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeliveryAddressID IS NOT NULL THEN (CASE WHEN e.[DeliveryAddressID] = @DeliveryAddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeliveryServiceID IS NOT NULL THEN (CASE WHEN e.[DeliveryServiceID] = @DeliveryServiceID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comments IS NOT NULL THEN (CASE WHEN e.[Comments] = @Comments THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedByID IS NOT NULL THEN (CASE WHEN e.[CreatedByID] = @CreatedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO