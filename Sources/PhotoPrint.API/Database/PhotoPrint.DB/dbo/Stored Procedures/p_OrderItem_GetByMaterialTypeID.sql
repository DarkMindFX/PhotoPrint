
CREATE PROCEDURE [dbo].[p_OrderItem_GetByMaterialTypeID]

	@MaterialTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OrderItem] c 
				WHERE
					[MaterialTypeID] = @MaterialTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OrderItem] e
		WHERE 
			[MaterialTypeID] = @MaterialTypeID	

	END
	ELSE
		SET @Found = 0;
END
