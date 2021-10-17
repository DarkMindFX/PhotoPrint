

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_Insert]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_Insert]
			@UserID BIGINT,
			@AddressID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserAddress]
	SELECT 
		@UserID,
		@AddressID,
		@IsPrimary
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserAddress] e
	WHERE
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN e.[AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO