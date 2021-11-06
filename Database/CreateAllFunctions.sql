USE [PhotoPrint]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetUserStatusIDByName]    Script Date: 10/16/2021 9:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetUserStatusIDByName] 
(
	@StatusName NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	
	SELECT @Result = [ID] FROM dbo.[UserStatus] WHERE StatusName = @StatusName


	RETURN @Result
END
GO
USE [PhotoPrint]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetUserTypeIDByName]    Script Date: 10/16/2021 9:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[fn_GetUserTypeIDByName] 
(
	@UserTypeName NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	
	SELECT @Result = [ID] FROM dbo.[UserType] WHERE UserTypeName = @UserTypeName


	RETURN @Result
END
GO
USE [PhotoPrint]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetUserStatusIDByName]    Script Date: 10/16/2021 9:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetUserStatusIDByName] 
(
	@StatusName NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	
	SELECT @Result = [ID] FROM dbo.[UserStatus] WHERE StatusName = @StatusName


	RETURN @Result
END
GO
USE [PhotoPrint]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetUserTypeIDByName]    Script Date: 10/16/2021 9:43:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[fn_GetUserTypeIDByName] 
(
	@UserTypeName NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	
	SELECT @Result = [ID] FROM dbo.[UserType] WHERE UserTypeName = @UserTypeName


	RETURN @Result
END
GO
