USE [PhotoPrint]
GO
/****** Object:  User [ppt_svc_api]    Script Date: 11/6/2021 11:14:11 AM ******/
CREATE USER [ppt_svc_api] FOR LOGIN [ppt_svc_api] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ppt_svc_api]
GO
