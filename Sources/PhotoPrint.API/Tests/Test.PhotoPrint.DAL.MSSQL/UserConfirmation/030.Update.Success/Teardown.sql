

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100010
DECLARE @ConfirmationCode NVARCHAR(50) = 'ConfirmationCode 2546c447e2c3469596b94af4f7fd2e00'
DECLARE @Comfirmed BIT = 1
DECLARE @ExpiresDate DATETIME = '8/23/2023 4:55:26 AM'
DECLARE @ConfirmationDate DATETIME = '8/23/2023 4:55:26 AM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updUserID BIGINT = 100009
DECLARE @updConfirmationCode NVARCHAR(50) = 'ConfirmationCode 8d89783eb4ed413ba87d23cb37bb8132'
DECLARE @updComfirmed BIT = 0
DECLARE @updExpiresDate DATETIME = '1/9/2021 5:22:26 AM'
DECLARE @updConfirmationDate DATETIME = '1/9/2021 5:22:26 AM'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[UserConfirmation]
				WHERE 
	(CASE WHEN @updUserID IS NOT NULL THEN (CASE WHEN [UserID] = @updUserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updConfirmationCode IS NOT NULL THEN (CASE WHEN [ConfirmationCode] = @updConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updComfirmed IS NOT NULL THEN (CASE WHEN [Comfirmed] = @updComfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updExpiresDate IS NOT NULL THEN (CASE WHEN [ExpiresDate] = @updExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updConfirmationDate IS NOT NULL THEN (CASE WHEN [ConfirmationDate] = @updConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[UserConfirmation]
	WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationCode IS NOT NULL THEN (CASE WHEN [ConfirmationCode] = @ConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Comfirmed IS NOT NULL THEN (CASE WHEN [Comfirmed] = @Comfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ExpiresDate IS NOT NULL THEN (CASE WHEN [ExpiresDate] = @ExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConfirmationDate IS NOT NULL THEN (CASE WHEN [ConfirmationDate] = @ConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[UserConfirmation]
	WHERE 
	(CASE WHEN @updUserID IS NOT NULL THEN (CASE WHEN [UserID] = @updUserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updConfirmationCode IS NOT NULL THEN (CASE WHEN [ConfirmationCode] = @updConfirmationCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updComfirmed IS NOT NULL THEN (CASE WHEN [Comfirmed] = @updComfirmed THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updExpiresDate IS NOT NULL THEN (CASE WHEN [ExpiresDate] = @updExpiresDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updConfirmationDate IS NOT NULL THEN (CASE WHEN [ConfirmationDate] = @updConfirmationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserConfirmation was not updated', 1
END