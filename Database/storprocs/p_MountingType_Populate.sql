
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
	SELECT 1,	'New' UNION
	SELECT 2,	'InProduction' UNION
	SELECT 3,	'Produced' UNION
	SELECT 4,	'PreparingDelivery' UNION
	SELECT 5,	'InDelivery' UNION
	SELECT 6,	'Delivered' UNION
	SELECT 7,	'Cancelled' UNION
	SELECT 8,	'Completed' UNION
	SELECT 9,	'PendingPayment' UNION
	SELECT 10,	'SentToProduction'

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
		VALUES (s.[ID], s.[MountingTypeName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.MountingType OFF;
	
	SET NOCOUNT ON;
END
GO
