
CREATE PROCEDURE dbo.p_PaymentMethod_Populate 
	
AS
BEGIN

	DECLARE @tblPaymentMethod AS TABLE (
		[ID] [bigint] NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
		[Description] [nvarchar](1000) NULL,
		[IsDeleted] [bit] NOT NULL
	)

	INSERT INTO @tblPaymentMethod
	SELECT 1, 'Visa', NULL, 0	UNION 
	SELECT 2, 'Mastercard', NULL, 0	UNION
	SELECT 3, 'PayPal', NULL, 0	UNION
	SELECT 4, 'Wire transfer', NULL, 0	UNION
	SELECT 5, 'Skrill', NULL, 0				


	SET IDENTITY_INSERT dbo.PaymentMethod ON;

	MERGE dbo.PaymentMethod AS t
	USING @tblPaymentMethod AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[Name] = s.[Name],
			t.[Description] = s.[Description],
			t.[IsDeleted] = s.[IsDeleted]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[Name],[Description],[IsDeleted]) VALUES (s.[ID],s.[Name],s.[Description],s.[IsDeleted])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.PaymentMethod OFF;
	
	SET NOCOUNT ON;
END
