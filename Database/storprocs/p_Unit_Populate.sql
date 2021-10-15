
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_Unit_Populate'))
   DROP PROC dbo.p_Unit_Populate
GO

CREATE PROCEDURE dbo.p_Unit_Populate 
	
AS
BEGIN

	DECLARE @tblUnit AS TABLE (
		[ID] [bigint] NOT NULL,
		[UnitName] [nvarchar](50) NOT NULL,
		[UnitAbbr] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](100) 
	)

	INSERT INTO @tblUnit
	SELECT 1, 'Square meter', 'm2', NULL	UNION 
	SELECT 2, 'Meter', 'm', NULL			UNION
	SELECT 3, 'Centimeter', 'cm', NULL		UNION
	SELECT 4, 'Square centimeter', 'cm2', NULL		UNION
	SELECT 5, 'Item', 'item', NULL			


	SET IDENTITY_INSERT dbo.Unit ON;

	MERGE dbo.Unit AS t
	USING @tblUnit AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[UnitName] = s.[UnitName],
			t.[UnitAbbr] = s.[UnitAbbr],
			t.[Description] = s.[Description]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[UnitName],[UnitAbbr],[Description]) VALUES (s.[ID], s.[UnitName],s.[UnitAbbr],s.[Description])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.Unit OFF;
	
	SET NOCOUNT ON;
END
GO
