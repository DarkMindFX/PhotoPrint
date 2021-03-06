




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserContact_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserContact_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_UserContact_GetDetails]
		@UserID BIGINT,	
		@ContactID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[UserContact] c 
				WHERE 
								[UserID] = @UserID	AND
								[ContactID] = @ContactID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[UserContact] e
		WHERE 
								[UserID] = @UserID	AND
								[ContactID] = @ContactID	
				END
	ELSE
		SET @Found = 0;
END
GO