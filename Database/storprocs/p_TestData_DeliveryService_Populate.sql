
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_TestData_DeliveryService_Populate'))
   DROP PROC dbo.p_TestData_DeliveryService_Populate
GO

CREATE PROCEDURE dbo.p_TestData_DeliveryService_Populate 
	
AS
BEGIN

	DECLARE @tblDeliveryService AS TABLE (
		[ID] [bigint] NOT NULL,
		[DeliveryServiceName] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](1000) NULL,
		[IsEnabled] [bit]
	)

	INSERT INTO @tblDeliveryService
	SELECT 100001,	'Self Pickup', NULL, 0	UNION
	SELECT 100002,	'DHL', NULL, 0	UNION
	SELECT 100003,	'UPS', NULL, 0	UNION
	SELECT 100004,	'Новая Почта', NULL, 1

	


	SET IDENTITY_INSERT dbo.DeliveryService ON;

	MERGE dbo.DeliveryService AS t
	USING @tblDeliveryService AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[DeliveryServiceName] = s.[DeliveryServiceName],
			t.[Description] = s.[Description],
			t.[IsEnabled] = s.[IsEnabled]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[DeliveryServiceName], [Description], [IsEnabled]) 
		VALUES (s.[ID], s.[DeliveryServiceName], s.[Description], s.[IsEnabled])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.DeliveryService OFF;
	
	SET NOCOUNT ON;
END
GO
