

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_UserConfirmation_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_UserConfirmation_Insert]
GO

CREATE PROCEDURE [dbo].[p_UserConfirmation_Insert]
			@ID BIGINT,
			@UserID BIGINT,
			@ConfirmationCode NVARCHAR(50),
			@Comfirmed BIT,
			@ExpiresDate DATETIME,
			@ConfirmationDate DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[UserConfirmation]
	SELECT 
		@UserID,
		@ConfirmationCode,
		@Comfirmed,
		@ExpiresDate,
		@ConfirmationDate
	
	

	SELECT
		e.*
	FROM
		[dbo].[UserConfirmation] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN e.[UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN e.[ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN e.[Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN e.[ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN e.[ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO