


DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name b4c3db4b76c24d608aec3a769148c800'
DECLARE @Description NVARCHAR(1000) = 'Description b4c3db4b76c24d608aec3a769148c800'
DECLARE @IsDeleted BIT = 0
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[PaymentMethod]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[PaymentMethod]
		(
	 [Name],
	 [Description],
	 [IsDeleted]
		)
	SELECT 		
			 @Name,
	 @Description,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[PaymentMethod] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
