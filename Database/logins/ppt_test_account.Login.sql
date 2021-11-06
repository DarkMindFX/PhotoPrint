USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [ppt_test_account]    Script Date: 11/6/2021 11:18:44 AM ******/
CREATE LOGIN [ppt_test_account] WITH PASSWORD=N'4vzkVi2aN3hhrhi7nouV87S1r3PozOge8Xii9HP3mls=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [ppt_test_account] DISABLE
GO


