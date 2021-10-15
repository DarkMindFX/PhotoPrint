
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_User_Populate'))
   DROP PROC dbo.p_TestData_User_Populate
GO

CREATE PROCEDURE dbo.p_TestData_User_Populate 
	
AS
BEGIN

	DECLARE @tblUser AS TABLE (
		[ID] [bigint] NOT NULL,
		[Login] [nvarchar](250) NOT NULL,
		[PwdHash] [nvarchar](250) NOT NULL,
		[Salt] [nvarchar](50) NOT NULL,
		[FirstName] [nvarchar](50) NOT NULL,
		[MiddleName] [nvarchar](50) NULL,
		[LastName] [nvarchar](50) NOT NULL,
		[FriendlyName] [nvarchar](50) NULL,
		[UserStatusID] [bigint] NOT NULL,
		[UserTypeID] [bigint] NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[ModifiedDate] [datetime] NULL,
		[ModifiedByID] [bigint] NULL
	)

	INSERT INTO @tblUser
	-- System user
	SELECT 100001, 'System', '#System==', '123SALT123BCD', 'SystemF', NULL, 'SystemL', 'System', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('System'), 
	'2021-10-01',
	NULL,
	NULL
	UNION 

	-- Admin user
	SELECT 100002, 'Admin', '#Admin==', '567SALT567WQA', 'AdminF', NULL, 'AdminL', 'Admin', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Admin'), 
	'2021-10-01',
	NULL,
	NULL
	UNION 

	-- Customers user
	SELECT 100003, 'LondonJ', '#LindonJ==', '567SALT567WQA', 'Lindon', NULL, 'Johnson', 'LindyJ', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Customer'), 
	'2021-10-01',
	NULL,
	NULL
	UNION 

	SELECT 100004, 'JohnK', '#JohnK==', 'ETERTERTR', 'John', NULL, 'Kennedy', 'JohnnyK', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Customer'), 
	'2021-10-01',
	NULL,
	NULL
	UNION 

	SELECT 100005, 'FranklinR', '#FranklinR==', '5656GHRED', 'Franklin', NULL, 'Roosevelt', 'Franklin', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Customer'), 
	'2021-10-01',
	NULL,
	NULL
	UNION 

	-- Manager users
	SELECT 100006, 'ManagerBill', '#ManagerBill==', '567SALT567WQA', 'Bill', NULL, 'Murrey', 'Billy', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Manager'), 
	'2021-10-01',
	NULL,
	NULL
	UNION 

	SELECT 100007, 'ManagerTed', '#ManagerTed==', 'ETERTERTR', 'Teddy', NULL, 'Atlas', 'Teddy', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Manager'),  
	'2021-10-01',
	NULL,
	NULL
	UNION 

	SELECT 100008, 'ManagerSam', '#ManagerSam==', '5656GHRED', 'Sammy', NULL, 'Schields', 'Sammy', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Manager'),  
	'2021-10-01',
	NULL,
	NULL
	UNION

	-- Partners
	SELECT 100009, 'PrinterBill', '#PrinterBill==', '567SALT567WQA', 'Billy', NULL, 'Ayliesh', 'Billy', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Partner'), 
	'2021-10-01',
	NULL,
	NULL
	UNION 

	SELECT 100010, 'PrinterTed', '#PrinterTed==', 'ETERTERTR', 'Ted', NULL, 'Turner', 'Teddy', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Partner'),  
	'2021-10-01',
	NULL,
	NULL
	UNION 

	SELECT 100011, 'PrinterSam', '#PrinterSam==', '5656GHRED', 'Sam', NULL, 'Smith', 'Sam', 
	dbo.fn_GetUserStatusIDByName('Activated'), 
	dbo.fn_GetUserTypeIDByName('Partner'),  
	'2021-10-01',
	NULL,
	NULL

	-- Adding users

	SET IDENTITY_INSERT dbo.[User] ON;

	MERGE dbo.[User] AS t
	USING @tblUser AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[Login] = s.[Login],
			t.[PwdHash] = s.[PwdHash],
			t.[Salt] = s.[Salt],
			t.[FirstName] = s.[FirstName],
			t.[MiddleName] = s.[MiddleName],
			t.[LastName] = s.[LastName],
			t.[FriendlyName] = s.[FriendlyName],
			t.[UserStatusID] = s.[UserStatusID],
			t.[UserTypeID] = s.[UserTypeID],
			t.[CreatedDate] = s.[CreatedDate],
			t.[ModifiedDate] = s.[ModifiedDate],
			t.[ModifiedByID] = s.[ModifiedByID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID], [Login], [PwdHash], [Salt], [FirstName], [MiddleName], [LastName], [FriendlyName], [UserStatusID], [UserTypeID], [CreatedDate], [ModifiedDate], [ModifiedByID]) 
		VALUES (s.[ID], s.[Login], s.[PwdHash], s.[Salt], s.[FirstName], s.[MiddleName], s.[LastName], s.[FriendlyName], s.[UserStatusID], s.[UserTypeID], s.[CreatedDate], s.[ModifiedDate], s.[ModifiedByID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.[User] OFF;

	-- Adding user's addresses
	
	SET NOCOUNT ON;
END
GO
