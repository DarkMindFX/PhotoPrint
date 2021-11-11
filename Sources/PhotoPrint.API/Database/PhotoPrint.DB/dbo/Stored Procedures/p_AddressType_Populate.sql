
CREATE PROCEDURE dbo.p_AddressType_Populate 
	
AS
BEGIN

	DECLARE @tblAddressType AS TABLE (
		[ID] [bigint] NOT NULL,
		[AddressTypeName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblAddressType
	SELECT 1, 'Main'	UNION 
	SELECT 2, 'Billing'		UNION
	SELECT 3, 'Delivery'	UNION
	SELECT 4, 'Office'		UNION
	SELECT 5, 'Production'	UNION
	SELECT 6, 'Warehouse'	

	SET IDENTITY_INSERT dbo.AddressType ON;

	MERGE dbo.AddressType AS t
	USING @tblAddressType AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET t.[AddressTypeName] = s.[AddressTypeName]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[AddressTypeName]) VALUES (s.[ID], s.[AddressTypeName])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.AddressType OFF;
	
	SET NOCOUNT ON;
END
