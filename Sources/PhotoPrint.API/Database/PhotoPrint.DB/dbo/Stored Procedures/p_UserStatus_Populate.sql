
CREATE PROCEDURE dbo.p_UserStatus_Populate 
	
AS
BEGIN

	DECLARE @tblUserStatus AS TABLE (
		[ID] [bigint] NOT NULL,
		[StatusName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblUserStatus
	SELECT 1, 'New'	UNION 
	SELECT 2, 'Activated'	UNION
	SELECT 3, 'Blocked'		UNION
	SELECT 4, 'Deleted'		

	SET IDENTITY_INSERT dbo.UserStatus ON;

	MERGE dbo.UserStatus AS t
	USING @tblUserStatus AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[StatusName] = s.[StatusName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[StatusName]) VALUES (s.[ID], s.[StatusName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.UserStatus OFF;
	
	SET NOCOUNT ON;
END
