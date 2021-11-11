
CREATE PROCEDURE [dbo].[p_ContactType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ContactType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ContactType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
