




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetByUserTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetByUserTypeID]
GO

CREATE PROCEDURE [dbo].[p_User_GetByUserTypeID]

	@UserTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[User] c 
				WHERE
					[UserTypeID] = @UserTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[User] e
		WHERE 
			[UserTypeID] = @UserTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO