


DECLARE @ID BIGINT = NULL
DECLARE @StatusName NVARCHAR(50) = 'StatusName 1169652d0836489988fbd4e493f36133'
DECLARE @IsDeleted BIT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[UserStatus]
				WHERE 
	(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN [StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[UserStatus]
		(
	 [StatusName],
	 [IsDeleted]
		)
	SELECT 		
			 @StatusName,
	 @IsDeleted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[UserStatus] e
WHERE
	(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN [StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
