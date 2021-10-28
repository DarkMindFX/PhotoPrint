

DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100001
DECLARE @ConfirmationCode NVARCHAR(50) = 'ConfirmationCode e15e5835df8e40f0bdfe6df436e2be0d'
DECLARE @Comfirmed BIT = 1
DECLARE @ExpiresDate DATETIME = '8/23/2023 4:55:26 AM'
DECLARE @ConfirmationDate DATETIME = '8/23/2023 4:55:26 AM'
 
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[UserConfirmation]
				WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN [ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN [Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN [ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN [ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[UserConfirmation]
	WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN [ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN [Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN [ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN [ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserConfirmation was not inserted', 1
END