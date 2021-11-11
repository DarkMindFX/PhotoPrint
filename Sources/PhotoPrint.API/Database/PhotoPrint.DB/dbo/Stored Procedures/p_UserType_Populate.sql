
CREATE PROCEDURE dbo.p_UserType_Populate 
	
AS
BEGIN

	DECLARE @tblUserType AS TABLE (
		[ID] [bigint] NOT NULL,
		[UserTypeName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblUserType
	SELECT 1, 'Customer'	UNION 
	SELECT 2, 'Manager'		UNION
	SELECT 3, 'Admin'		UNION
	SELECT 4, 'System'		UNION
	SELECT 5, 'Partner'		

	SET IDENTITY_INSERT dbo.UserType ON;

	MERGE dbo.UserType AS t
	USING @tblUserType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[UserTypeName] = s.[UserTypeName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[UserTypeName]) VALUES (s.[ID], s.[UserTypeName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.UserType OFF;
	
	SET NOCOUNT ON;
END
