

DECLARE @ID BIGINT = NULL
DECLARE @Login NVARCHAR(250) = 'Login 31be429314b3495f89f6df705e5b0331'
DECLARE @PwdHash NVARCHAR(250) = 'PwdHash 31be429314b3495f89f6df705e5b0331'
DECLARE @Salt NVARCHAR(50) = 'Salt 31be429314b3495f89f6df705e5b0331'
DECLARE @FirstName NVARCHAR(50) = 'FirstName 31be429314b3495f89f6df705e5b0331'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName 31be429314b3495f89f6df705e5b0331'
DECLARE @LastName NVARCHAR(50) = 'LastName 31be429314b3495f89f6df705e5b0331'
DECLARE @FriendlyName NVARCHAR(50) = 'FriendlyName 31be429314b3495f89f6df705e5b0331'
DECLARE @UserStatusID BIGINT = 4
DECLARE @UserTypeID BIGINT = 5
DECLARE @CreatedDate DATETIME = '10/19/2020 8:21:34 AM'
DECLARE @ModifiedDate DATETIME = '10/19/2020 8:21:34 AM'
DECLARE @ModifiedByID BIGINT = 100008
 


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
