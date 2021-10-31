

-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @UserID BIGINT = 100001
DECLARE @ConfirmationCode NVARCHAR(50) = 'ConfirmationCode ff10cbdfa6734375991c45bbb40762f7'
DECLARE @Comfirmed BIT = 0
DECLARE @ExpiresDate DATETIME = '8/21/2021 1:57:34 PM'
DECLARE @ConfirmationDate DATETIME = '8/21/2021 1:57:34 PM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updUserID BIGINT = 100003
DECLARE @updConfirmationCode NVARCHAR(50) = 'ConfirmationCode 9480c4a0802a45fea32c47ad80143fe8'
DECLARE @updComfirmed BIT = 0
DECLARE @updExpiresDate DATETIME = '7/8/2019 5:32:34 AM'
DECLARE @updConfirmationDate DATETIME = '7/8/2019 5:32:34 AM'
 

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