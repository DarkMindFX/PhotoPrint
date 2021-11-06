


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Order_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Order_Update]
GO

CREATE PROCEDURE [dbo].[p_Order_Update]
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


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Order]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Order]
		SET
									[ManagerID] = IIF( @ManagerID IS NOT NULL, @ManagerID, [ManagerID] ) ,
									[UserID] = IIF( @UserID IS NOT NULL, @UserID, [UserID] ) ,
									[ContactID] = IIF( @ContactID IS NOT NULL, @ContactID, [ContactID] ) ,
									[DeliveryAddressID] = IIF( @DeliveryAddressID IS NOT NULL, @DeliveryAddressID, [DeliveryAddressID] ) ,
									[DeliveryServiceID] = IIF( @DeliveryServiceID IS NOT NULL, @DeliveryServiceID, [DeliveryServiceID] ) ,
									[Comments] = IIF( @Comments IS NOT NULL, @Comments, [Comments] ) ,
									[IsDeleted] = IIF( @IsDeleted IS NOT NULL, @IsDeleted, [IsDeleted] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[CreatedByID] = IIF( @CreatedByID IS NOT NULL, @CreatedByID, [CreatedByID] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Order was not found', 1;
	END	

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