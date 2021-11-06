


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_AddressType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_AddressType_Insert]
GO

CREATE PROCEDURE [dbo].[p_AddressType_Insert]
			@ID BIGINT,
			@AddressTypeName NVARCHAR(50),
			@IsDeleted BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[AddressType]
	SELECT 
		@AddressTypeName,
		@IsDeleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[AddressType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AddressTypeName IS NOT NULL THEN (CASE WHEN e.[AddressTypeName] = @AddressTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN e.[IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO