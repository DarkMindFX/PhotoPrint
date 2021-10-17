




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserAddress_GetByUserID', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserAddress_GetByUserID]
GO

CREATE PROCEDURE [dbo].[p_UserAddress_GetByUserID]

	@UserID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserAddress] c 
				WHERE
					[UserID] = @UserID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserAddress] e
		WHERE 
			[UserID] = @UserID	

	END
	ELSE
		SET @Found = 0;
END
GO