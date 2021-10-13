
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_FrameType_Populate'))
   DROP PROC dbo.p_FrameType_Populate
GO

CREATE PROCEDURE dbo.p_TestData_FrameType_Populate 
	
AS
BEGIN

	DECLARE @tblFrameType AS TABLE (
		[ID] [bigint] NOT NULL,
		[FrameTypeName] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](1000) NOT NULL,
		[ThumbnailUrl] [nvarchar](1000) NOT NULL,
		[IsDeleted] [bit]
	)

	INSERT INTO @tblFrameType
	SELECT 100001,	'None', '', 'https://picsum.photos/seed/pprint-frame-none/100/100',  0	UNION
	SELECT 100002,	'WoodenThin', '', 'https://picsum.photos/seed/pprint-frame-woodenthin/100/100',	 0	UNION
	SELECT 100003,	'WoodenThick', '', 'https://picsum.photos/seed/pprint-frame-woodenthick/100/100', 0	UNION
	SELECT 100004,	'Aluminium', '', 'https://picsum.photos/seed/pprint-frame-aluminium/100/100', 0	UNION
	SELECT 100005,	'RetroGold', '', 'https://picsum.photos/seed/pprint-frame-retrogold/100/100',  0 UNION
	SELECT 100006,	'RetroSilver', '', 'https://picsum.photos/seed/pprint-frame-retrosilver/100/100',  0 UNION
	SELECT 100007,	'RetroBronze', '', 'https://picsum.photos/seed/pprint-frame-retrobronze/100/100',  0 UNION
	SELECT 100008,	'OldCopper', '', 'https://picsum.photos/seed/pprint-frame-oldcopper/100/100',  0 


	SET IDENTITY_INSERT dbo.FrameType ON;

	MERGE dbo.FrameType AS t
	USING @tblFrameType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[FrameTypeName] = s.[FrameTypeName],
			t.[Description] = s.[Description],
			t.[ThumbnailUrl] = s.[ThumbnailUrl],
			t.[IsDeleted] = s.[IsDeleted]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[FrameTypeName], [Description], [ThumbnailUrl], [IsDeleted]) 
		VALUES (s.[ID], s.[FrameTypeName], s.[Description], s.[ThumbnailUrl], s.[IsDeleted])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.FrameType OFF;
	
	SET NOCOUNT ON;
END
GO
