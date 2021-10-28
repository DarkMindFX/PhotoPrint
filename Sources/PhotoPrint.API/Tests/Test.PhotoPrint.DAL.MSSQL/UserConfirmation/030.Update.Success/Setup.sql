

DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100010
DECLARE @ConfirmationCode NVARCHAR(50) = 'ConfirmationCode 2546c447e2c3469596b94af4f7fd2e00'
DECLARE @Comfirmed BIT = 1
DECLARE @ExpiresDate DATETIME = '8/23/2023 4:55:26 AM'
DECLARE @ConfirmationDate DATETIME = '8/23/2023 4:55:26 AM'
 


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
	INSERT INTO [dbo].[UserConfirmation]
		(
	 [UserID],
	 [ConfirmationCode],
	 [Comfirmed],
	 [ExpiresDate],
	 [ConfirmationDate]
		)
	SELECT 		
			 @UserID,
	 @ConfirmationCode,
	 @Comfirmed,
	 @ExpiresDate,
	 @ConfirmationDate
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[UserConfirmation] e
WHERE
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN [ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN [Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN [ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN [ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
