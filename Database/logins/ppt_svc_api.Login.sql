USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [ppt_svc_api]    Script Date: 11/6/2021 11:17:38 AM ******/
CREATE LOGIN [ppt_svc_api] WITH PASSWORD=N'PPTServiceApi2021!', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO



