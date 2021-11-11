

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
