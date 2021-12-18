
/******************** Populating Reference Data **************************/
PRINT 'Populating reference data'
EXEC dbo.p_AddressType_Populate

EXEC dbo.p_Country_Populate

EXEC dbo.p_Region_Populate

EXEC dbo.p_City_Populate

EXEC dbo.p_ContactType_Populate

EXEC dbo.p_Currency_Populate

EXEC dbo.p_OrderStatus_Populate

EXEC dbo.p_PaymentMethod_Populate

EXEC dbo.p_Unit_Populate

EXEC dbo.p_UserStatus_Populate

EXEC dbo.p_UserType_Populate

PRINT 'Populating test data'
IF(NOT EXISTS(SELECT ID FROM [dbo].[User] ))
BEGIN
	EXEC p_TestData_Populate '/sql/testdata/'
END
ELSE
BEGIN
	PRINT 'Test data present - skipping'
END

PRINT('Done')