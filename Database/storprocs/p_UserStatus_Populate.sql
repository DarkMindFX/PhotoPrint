
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_UserStatus_Populate'))
   DROP PROC dbo.p_UserStatus_Populate
GO

CREATE PROCEDURE dbo.p_UserStatus_Populate 
	
AS
BEGIN

	DECLARE @tblUserStatus AS TABLE (
		[ID] [bigint] NOT NULL,
		[UserStatusName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblUserStatus
	SELECT 1, 'New'	UNION 
	SELECT 2, 'Active'		UNION
	SELECT 3, 'Blocked'		UNION
	SELECT 4, 'Deleted'		

	SET IDENTITY_INSERT dbo.UserStatus ON;

	MERGE dbo.UserStatus AS t
	USING @tblUserStatus AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[UserStatusName] = s.[UserStatusName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[UserStatusName]) VALUES (s.[ID], s.[UserStatusName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.UserStatus OFF;
	
	SET NOCOUNT ON;
END
GO
