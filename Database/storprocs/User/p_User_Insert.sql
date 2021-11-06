


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_Insert]
GO

CREATE PROCEDURE [dbo].[p_User_Insert]
			@ID BIGINT,
			@Login NVARCHAR(250),
			@PwdHash NVARCHAR(250),
			@Salt NVARCHAR(50),
			@FirstName NVARCHAR(50),
			@MiddleName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@FriendlyName NVARCHAR(50),
			@UserStatusID BIGINT,
			@UserTypeID BIGINT,
			@CreatedDate DATETIME,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[User]
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
	
	

	SELECT
		e.*
	FROM
		[dbo].[User] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN e.[Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN e.[PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN e.[Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN e.[MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN e.[FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserStatusID IS NOT NULL THEN (CASE WHEN e.[UserStatusID] = @UserStatusID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UserTypeID IS NOT NULL THEN (CASE WHEN e.[UserTypeID] = @UserTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO