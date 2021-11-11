
CREATE PROCEDURE [dbo].[p_Contact_GetByContactTypeID]

	@ContactTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Contact] c 
				WHERE
					[ContactTypeID] = @ContactTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Contact] e
		WHERE 
			[ContactTypeID] = @ContactTypeID	

	END
	ELSE
		SET @Found = 0;
END
