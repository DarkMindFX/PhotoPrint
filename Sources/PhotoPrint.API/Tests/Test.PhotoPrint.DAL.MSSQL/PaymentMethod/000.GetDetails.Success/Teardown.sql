

DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 834a976410274e98973c69b60b685f4c'
DECLARE @Description NVARCHAR(1000) = 'Description 834a976410274e98973c69b60b685f4c'
DECLARE @IsDeleted BIT = 0
 

DELETE FROM [PaymentMethod]
FROM 
	[dbo].[PaymentMethod] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 