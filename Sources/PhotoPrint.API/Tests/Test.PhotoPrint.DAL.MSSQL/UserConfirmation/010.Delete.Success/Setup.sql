


DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100007
DECLARE @ConfirmationCode NVARCHAR(50) = 'ConfirmationCode d4940b73900e4e75b6e45890d296127c'
DECLARE @Comfirmed BIT = 0
DECLARE @ExpiresDate DATETIME = '6/26/2022 4:15:40 AM'
DECLARE @ConfirmationDate DATETIME = '6/26/2022 4:15:40 AM'
 


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
