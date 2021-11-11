
CREATE PROCEDURE dbo.p_Region_Populate 
	
AS
BEGIN

	DECLARE @tblRegion AS TABLE (
		[ID] [bigint] NOT NULL,
		[RegionName] [nvarchar](50) NOT NULL,
		[CountryID] [bigint] NOT NULL
	)

	INSERT INTO @tblRegion
	-- UKRAINE
	SELECT 1, 'Kharkivska obl.', 232	UNION 
	SELECT 2, 'Kyivska obl.', 232		UNION
	SELECT 3, 'Dniprovska obl.', 232 UNION
	SELECT 4, 'Rivnenska obl.', 232 UNION
	SELECT 5, 'Zhytomyrska obl.', 232 UNION
	SELECT 6, 'All Ukraine', 232 UNION
	-- POLAND
	SELECT 7, 'All Poland', 176 UNION
	SELECT 8, 'Pomorske voevodship', 176 UNION
	SELECT 9, 'Mazowieckie voevodship', 176

	SET IDENTITY_INSERT dbo.Region ON;

	MERGE dbo.Region AS t
	USING @tblRegion AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[RegionName] = s.[RegionName],
			t.[CountryID] = s.[CountryID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[RegionName], [CountryID]) VALUES (s.[ID], s.[RegionName], s.[CountryID])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.Region OFF;
	
	SET NOCOUNT ON;
END
