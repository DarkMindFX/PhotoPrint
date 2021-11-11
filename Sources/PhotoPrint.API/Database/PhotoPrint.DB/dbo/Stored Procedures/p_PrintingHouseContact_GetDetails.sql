
CREATE PROCEDURE [dbo].[p_PrintingHouseContact_GetDetails]
		@PrintingHouseID BIGINT,	
		@ContactID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseContact] c 
				WHERE 
								[PrintingHouseID] = @PrintingHouseID	AND
								[ContactID] = @ContactID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[PrintingHouseContact] e
		WHERE 
								[PrintingHouseID] = @PrintingHouseID	AND
								[ContactID] = @ContactID	
				END
	ELSE
		SET @Found = 0;
END
