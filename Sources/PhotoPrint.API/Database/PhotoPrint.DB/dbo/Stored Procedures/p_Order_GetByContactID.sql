
CREATE PROCEDURE [dbo].[p_Order_GetByContactID]

	@ContactID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Order] c 
				WHERE
					[ContactID] = @ContactID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Order] e
		WHERE 
			[ContactID] = @ContactID	

	END
	ELSE
		SET @Found = 0;
END
