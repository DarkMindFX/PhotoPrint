

DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100003
DECLARE @ConfirmationCode NVARCHAR(50) = 'ConfirmationCode 4613055313c54e6e8bc76c5fa201c97b'
DECLARE @Comfirmed BIT = 1
DECLARE @ExpiresDate DATETIME = '10/18/2019 7:33:26 AM'
DECLARE @ConfirmationDate DATETIME = '10/18/2019 7:33:26 AM'
 

DELETE FROM [UserConfirmation]
FROM 
	[dbo].[UserConfirmation] e
WHERE
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN [ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN [Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN [ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN [ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
