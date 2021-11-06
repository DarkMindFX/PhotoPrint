


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseContact_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseContact_Update]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseContact_Update]
			@PrintingHouseID BIGINT,
			@ContactID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseContact]
					WHERE 
												[PrintingHouseID] = @PrintingHouseID	AND
												[ContactID] = @ContactID	
							))
	BEGIN
		UPDATE [dbo].[PrintingHouseContact]
		SET
									[PrintingHouseID] = IIF( @PrintingHouseID IS NOT NULL, @PrintingHouseID, [PrintingHouseID] ) ,
									[ContactID] = IIF( @ContactID IS NOT NULL, @ContactID, [ContactID] ) ,
									[IsPrimary] = IIF( @IsPrimary IS NOT NULL, @IsPrimary, [IsPrimary] ) 
						WHERE 
												[PrintingHouseID] = @PrintingHouseID	AND
												[ContactID] = @ContactID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PrintingHouseContact was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseContact] e
	WHERE
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ContactID IS NOT NULL THEN (CASE WHEN e.[ContactID] = @ContactID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO