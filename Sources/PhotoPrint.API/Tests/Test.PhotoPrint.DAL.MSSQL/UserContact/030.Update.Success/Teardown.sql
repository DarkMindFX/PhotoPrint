

-- original values --
DECLARE @UserID BIGINT = 100006
DECLARE @ContactID BIGINT = 100021
DECLARE @IsPrimary BIT = 0
 
-- updated values --

DECLARE @updUserID BIGINT = 100006
DECLARE @updContactID BIGINT = 100021
DECLARE @updIsPrimary BIT = 1
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[UserContact]
				WHERE 
	(CASE WHEN @updUserID IS NOT NULL THEN (CASE WHEN [UserID] = @updUserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @updContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @updIsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[UserContact]
	WHERE 
	(CASE WHEN @UserID IS NOT NULL THEN (CASE WHEN [UserID] = @UserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[UserContact]
	WHERE 
	(CASE WHEN @updUserID IS NOT NULL THEN (CASE WHEN [UserID] = @updUserID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updContactID IS NOT NULL THEN (CASE WHEN [ContactID] = @updContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsPrimary IS NOT NULL THEN (CASE WHEN [IsPrimary] = @updIsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'UserContact was not updated', 1
END