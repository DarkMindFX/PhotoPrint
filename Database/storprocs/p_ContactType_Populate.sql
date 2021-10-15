
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_ContactType_Populate'))
   DROP PROC dbo.p_ContactType_Populate
GO

CREATE PROCEDURE dbo.p_ContactType_Populate 
	
AS
BEGIN

	DECLARE @tblContactType AS TABLE (
		[ID] [bigint] NOT NULL,
		[ContactTypeName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblContactType
	SELECT 1, 'Email'	UNION 
	SELECT 2, 'Phone'		UNION
	SELECT 3, 'WhatsApp'		UNION
	SELECT 4, 'Viber'		UNION
	SELECT 5, 'Skype'	UNION
	SELECT 6, 'Telegram'

	SET IDENTITY_INSERT dbo.ContactType ON;

	MERGE dbo.ContactType AS t
	USING @tblContactType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[ContactTypeName] = s.[ContactTypeName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[ContactTypeName]) VALUES (s.[ID], s.[ContactTypeName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.ContactType OFF;
	
	SET NOCOUNT ON;
END
GO
