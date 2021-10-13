
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_DeliveryType_Populate'))
   DROP PROC dbo.p_TestData_DeliveryType_Populate
GO

CREATE PROCEDURE dbo.p_TestData_DeliveryType_Populate 
	
AS
BEGIN

	DECLARE @tblDeliveryType AS TABLE (
		[ID] [bigint] NOT NULL,
		[DeliveryTypeName] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](1000) NULL
	)

	INSERT INTO @tblDeliveryType
	SELECT 100001,	'Self Pickup', NULL	UNION
	SELECT 100002,	'DHL', NULL	UNION
	SELECT 100003,	'UPS', NULL	UNION
	SELECT 100004,	'Nova Pochta', NULL

	


	SET IDENTITY_INSERT dbo.DeliveryType ON;

	MERGE dbo.DeliveryType AS t
	USING @tblDeliveryType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[DeliveryTypeName] = s.[DeliveryTypeName],
			t.[Description] = s.[Description]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[DeliveryTypeName], [Description], [ThumbnailUrl], [IsDeleted]) 
		VALUES (s.[ID], s.[DeliveryTypeName], s.[Description], s.[ThumbnailUrl], s.[IsDeleted])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.DeliveryType OFF;
	
	SET NOCOUNT ON;
END
GO
