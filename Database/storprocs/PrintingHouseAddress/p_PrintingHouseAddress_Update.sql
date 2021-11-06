


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_PrintingHouseAddress_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_PrintingHouseAddress_Update]
GO

CREATE PROCEDURE [dbo].[p_PrintingHouseAddress_Update]
			@PrintingHouseID BIGINT,
			@AddressID BIGINT,
			@IsPrimary BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[PrintingHouseAddress]
					WHERE 
												[PrintingHouseID] = @PrintingHouseID	AND
												[AddressID] = @AddressID	
							))
	BEGIN
		UPDATE [dbo].[PrintingHouseAddress]
		SET
									[PrintingHouseID] = IIF( @PrintingHouseID IS NOT NULL, @PrintingHouseID, [PrintingHouseID] ) ,
									[AddressID] = IIF( @AddressID IS NOT NULL, @AddressID, [AddressID] ) ,
									[IsPrimary] = IIF( @IsPrimary IS NOT NULL, @IsPrimary, [IsPrimary] ) 
						WHERE 
												[PrintingHouseID] = @PrintingHouseID	AND
												[AddressID] = @AddressID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'PrintingHouseAddress was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[PrintingHouseAddress] e
	WHERE
				(CASE WHEN @PrintingHouseID IS NOT NULL THEN (CASE WHEN e.[PrintingHouseID] = @PrintingHouseID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressID IS NOT NULL THEN (CASE WHEN e.[AddressID] = @AddressID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsPrimary IS NOT NULL THEN (CASE WHEN e.[IsPrimary] = @IsPrimary THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO