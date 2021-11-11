


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
