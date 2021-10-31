

DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100005
DECLARE @ConfirmationCode NVARCHAR(50) = 'ConfirmationCode 24d575a85e4248c399447fa00a0e4c43'
DECLARE @Comfirmed BIT = 1
DECLARE @ExpiresDate DATETIME = '1/3/2024 5:57:34 PM'
DECLARE @ConfirmationDate DATETIME = '1/3/2024 5:57:34 PM'
 

DELETE FROM [UserConfirmation]
FROM 
	[dbo].[UserConfirmation] e
WHERE
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN [ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN [Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN [ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN [ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
