


DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100002
DECLARE @ConfirmationCode NVARCHAR(50) = 'ConfirmationCode c12072be7fe343a59823abc29eac31ca'
DECLARE @Comfirmed BIT = 0
DECLARE @ExpiresDate DATETIME = '8/15/2019 6:28:40 PM'
DECLARE @ConfirmationDate DATETIME = '8/15/2019 6:28:40 PM'
 


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
