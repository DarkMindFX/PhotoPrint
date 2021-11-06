


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_Update]
GO

CREATE PROCEDURE [dbo].[p_User_Update]
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


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[User]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[User]
		SET
									[Login] = IIF( @Login IS NOT NULL, @Login, [Login] ) ,
									[PwdHash] = IIF( @PwdHash IS NOT NULL, @PwdHash, [PwdHash] ) ,
									[Salt] = IIF( @Salt IS NOT NULL, @Salt, [Salt] ) ,
									[FirstName] = IIF( @FirstName IS NOT NULL, @FirstName, [FirstName] ) ,
									[MiddleName] = IIF( @MiddleName IS NOT NULL, @MiddleName, [MiddleName] ) ,
									[LastName] = IIF( @LastName IS NOT NULL, @LastName, [LastName] ) ,
									[FriendlyName] = IIF( @FriendlyName IS NOT NULL, @FriendlyName, [FriendlyName] ) ,
									[UserStatusID] = IIF( @UserStatusID IS NOT NULL, @UserStatusID, [UserStatusID] ) ,
									[UserTypeID] = IIF( @UserTypeID IS NOT NULL, @UserTypeID, [UserTypeID] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'User was not found', 1;
	END	

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