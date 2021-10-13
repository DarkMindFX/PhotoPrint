
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_MaterialType_Populate'))
   DROP PROC dbo.p_MaterialType_Populate
GO

CREATE PROCEDURE dbo.p_TestData_MaterialType_Populate 
	
AS
BEGIN

	DECLARE @tblMaterialType AS TABLE (
		[ID] [bigint] NOT NULL,
		[MaterialTypeName] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](1000) NOT NULL,
		[ThumbnailUrl] [nvarchar](1000) NOT NULL,
		[IsDeleted] [bit]
	)

	INSERT INTO @tblMaterialType
	SELECT 100001,	'Paper', '', 'https://picsum.photos/seed/pprint-material-paper/100/100',  0	UNION
	SELECT 100002,	'Wood', '', 'https://picsum.photos/seed/pprint-material-wood/100/100',	 0	UNION
	SELECT 100003,	'Plastic', '', 'https://picsum.photos/seed/pprint-material-pastic/100/100', 0	UNION
	SELECT 100004,	'EcoMaterial', '', 'https://picsum.photos/seed/pprint-material-ecomaterial/100/100', 0	UNION
	SELECT 100005,	'Metal', '', 'https://picsum.photos/seed/pprint-material-metal/100/100',  0 UNION
	SELECT 100006,	'Glass', '', 'https://picsum.photos/seed/pprint-material-glass/100/100',  0 


	SET IDENTITY_INSERT dbo.MaterialType ON;

	MERGE dbo.MaterialType AS t
	USING @tblMaterialType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[MaterialTypeName] = s.[MaterialTypeName],
			t.[Description] = s.[Description],
			t.[ThumbnailUrl] = s.[ThumbnailUrl],
			t.[IsDeleted] = s.[IsDeleted]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[MaterialTypeName], [Description], [ThumbnailUrl], [IsDeleted]) 
		VALUES (s.[ID], s.[MaterialTypeName], s.[Description], s.[ThumbnailUrl], s.[IsDeleted])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.MaterialType OFF;
	
	SET NOCOUNT ON;
END
GO
