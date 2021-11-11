
CREATE PROCEDURE [dbo].[p_FrameType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[FrameType] e
END
