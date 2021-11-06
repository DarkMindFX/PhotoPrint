USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [ppt_svc_api]    Script Date: 11/6/2021 11:17:38 AM ******/
CREATE LOGIN [ppt_svc_api] WITH PASSWORD=N'ue7dBbseA/rhF3A+YEg0NkkqI8mPsuXc0WPWuKJYW5k=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [ppt_svc_api] DISABLE
GO


