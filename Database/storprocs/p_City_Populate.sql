
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_City_Populate'))
   DROP PROC dbo.p_City_Populate
GO

CREATE PROCEDURE dbo.p_City_Populate 
	
AS
BEGIN

	DECLARE @tblCity AS TABLE (
		[ID] [bigint] NOT NULL,
		[CityName] [nvarchar](50) NOT NULL,
		[RegionID] [bigint] NOT NULL
	)

	INSERT INTO @tblCity
	-- UKRAINE Kharkivska obl
	SELECT 1, 'Kharkiv', 1	UNION 
	SELECT 2, 'Chuguev', 1	UNION
	-- UKRAINE Kyivska obl
	SELECT 3, 'Kyiv', 2		UNION
	SELECT 4, 'Boryspil', 2		UNION
	-- UKRAINE Dniprovska obl
	SELECT 5, 'Dnipro', 3 UNION
	-- UKRAINE Rivnenska obl
	SELECT 6, 'Rivne', 4 UNION
	-- UKRAINE Zhytomyrska obl
	SELECT 7, 'Zhytomyr', 5 UNION
	-- POLAND Pomorskie
	SELECT 8, 'Gdansk', 8 UNION
	SELECT 9, 'Gdynia', 8 UNION
	-- POLAND MAzowieckie
	SELECT 10, 'Warszawa', 9

	SET IDENTITY_INSERT dbo.City ON;

	MERGE dbo.City AS t
	USING @tblCity AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[CityName] = s.[CityName],
			t.[RegionID] = s.[RegionID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[CityName], [RegionID]) VALUES (s.[ID], s.[CityName], s.[RegionID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.City OFF;
	
	SET NOCOUNT ON;
END
GO
