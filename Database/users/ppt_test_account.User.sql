USE [PhotoPrint]
GO
/****** Object:  User [ppt_test_account]    Script Date: 11/6/2021 11:14:12 AM ******/
CREATE USER [ppt_test_account] FOR LOGIN [ppt_test_account] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ppt_test_account]
GO
