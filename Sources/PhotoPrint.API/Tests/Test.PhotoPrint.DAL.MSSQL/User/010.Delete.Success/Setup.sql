

DECLARE @ID BIGINT = NULL
DECLARE @Login NVARCHAR(250) = 'Login 5d5b187a59bb4185914b89109c7e5b1d'
DECLARE @PwdHash NVARCHAR(250) = 'PwdHash 5d5b187a59bb4185914b89109c7e5b1d'
DECLARE @Salt NVARCHAR(50) = 'Salt 5d5b187a59bb4185914b89109c7e5b1d'
DECLARE @FirstName NVARCHAR(50) = 'FirstName 5d5b187a59bb4185914b89109c7e5b1d'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName 5d5b187a59bb4185914b89109c7e5b1d'
DECLARE @LastName NVARCHAR(50) = 'LastName 5d5b187a59bb4185914b89109c7e5b1d'
DECLARE @FriendlyName NVARCHAR(50) = 'FriendlyName 5d5b187a59bb4185914b89109c7e5b1d'
DECLARE @UserStatusID BIGINT = 1
DECLARE @UserTypeID BIGINT = 5
DECLARE @CreatedDate DATETIME = '12/19/2022 8:09:49 AM'
DECLARE @ModifiedDate DATETIME = '12/19/2022 8:09:49 AM'
DECLARE @ModifiedByID BIGINT = 713841
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[User]
				WHERE 
	(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN [Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN [Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN [MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN [LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN [FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserStatusID IS NOT NULL THEN (CASE WHEN [UserStatusID] = @UserStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserTypeID IS NOT NULL THEN (CASE WHEN [UserTypeID] = @UserTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[User]
		(
	 [Login],
	 [PwdHash],
	 [Salt],
	 [FirstName],
	 [MiddleName],
	 [LastName],
	 [FriendlyName],
	 [UserStatusID],
	 [UserTypeID],
	 [CreatedDate],
	 [ModifiedDate],
	 [ModifiedByID]
		)
	SELECT 		
			 @Login,
	 @PwdHash,
	 @Salt,
	 @FirstName,
	 @MiddleName,
	 @LastName,
	 @FriendlyName,
	 @UserStatusID,
	 @UserTypeID,
	 @CreatedDate,
	 @ModifiedDate,
	 @ModifiedByID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[User] e
WHERE
	(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN [Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN [Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN [MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN [LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN [FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserStatusID IS NOT NULL THEN (CASE WHEN [UserStatusID] = @UserStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UserTypeID IS NOT NULL THEN (CASE WHEN [UserTypeID] = @UserTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
