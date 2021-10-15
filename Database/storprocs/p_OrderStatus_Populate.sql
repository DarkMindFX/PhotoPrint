
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_OrderStatus_Populate'))
   DROP PROC dbo.p_OrderStatus_Populate
GO

CREATE PROCEDURE dbo.p_OrderStatus_Populate 
	
AS
BEGIN

	DECLARE @tblOrderStatus AS TABLE (
		[ID] [bigint] NOT NULL,
		[OrderStatusName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblOrderStatus
	SELECT 1,	'New' UNION
	SELECT 2,	'ProductionReady' UNION
	SELECT 3,	'SentToProduction' UNION
	SELECT 4,	'InProduction' UNION
	SELECT 5,	'Produced' UNION
	SELECT 6,	'SentToOffice' UNION
	SELECT 7,	'PrepareDelivery' UNION
	SELECT 8,	'AwaitSending' UNION
	SELECT 9,	'Sent' UNION
	SELECT 10,	'Received' UNION
	SELECT 11,	'Cancelled'

	SET IDENTITY_INSERT dbo.OrderStatus ON;

	MERGE dbo.OrderStatus AS t
	USING @tblOrderStatus AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[OrderStatusName] = s.[OrderStatusName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[OrderStatusName]) VALUES (s.[ID], s.[OrderStatusName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.OrderStatus OFF;
	
	SET NOCOUNT ON;
END
GO
