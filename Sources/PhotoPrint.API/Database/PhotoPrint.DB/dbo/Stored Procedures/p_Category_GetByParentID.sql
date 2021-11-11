
CREATE PROCEDURE [dbo].[p_Category_GetByParentID]

	@ParentID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Category] c 
				WHERE
					[ParentID] = @ParentID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Category] e
		WHERE 
			[ParentID] = @ParentID	

	END
	ELSE
		SET @Found = 0;
END
