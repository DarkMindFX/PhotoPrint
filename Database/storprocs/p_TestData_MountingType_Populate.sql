
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_MountingType_Populate'))
   DROP PROC dbo.p_MountingType_Populate
GO

CREATE PROCEDURE dbo.p_TestData_MountingType_Populate 
	
AS
BEGIN

	DECLARE @tblMountingType AS TABLE (
		[ID] [bigint] NOT NULL,
		[MountingTypeName] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](1000) NOT NULL,
		[ThumbnailUrl] [nvarchar](1000) NOT NULL,
		[IsDeleted] [bit]
	)

	INSERT INTO @tblMountingType
	SELECT 100001,	'None', '', 'https://picsum.photos/seed/pprint-mount-none/100/100',  0	UNION
	SELECT 100002,	'Nail', '', 'https://picsum.photos/seed/pprint-mount-nail/100/100',	 0	UNION
	SELECT 100003,	'Screw', '', 'https://picsum.photos/seed/pprint-mount-screw/100/100', 0	UNION
	SELECT 100004,	'Complex', '', 'https://picsum.photos/seed/pprint-mount-complex/100/100', 0	UNION
	SELECT 100005,	'Wire', '', 'https://picsum.photos/seed/pprint-mount-wire/100/100',  0


	SET IDENTITY_INSERT dbo.MountingType ON;

	MERGE dbo.MountingType AS t
	USING @tblMountingType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[MountingTypeName] = s.[MountingTypeName],
			t.[Description] = s.[Description],
			t.[ThumbnailUrl] = s.[ThumbnailUrl],
			t.[IsDeleted] = s.[IsDeleted]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[MountingTypeName], [Description], [ThumbnailUrl], [IsDeleted]) 
		VALUES (s.[ID], s.[MountingTypeName], s.[Description], s.[ThumbnailUrl], s.[IsDeleted])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.MountingType OFF;
	
	SET NOCOUNT ON;
END
GO
