


DECLARE @ID BIGINT = NULL
DECLARE @StatusName NVARCHAR(50) = 'StatusName 9bdb178da22445ee9e509dd13cdb4a2a'
DECLARE @IsDeleted BIT = 1
 

DELETE FROM [UserStatus]
FROM 
	[dbo].[UserStatus] e
WHERE
	(CASE WHEN @StatusName IS NOT NULL THEN (CASE WHEN [StatusName] = @StatusName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDeleted IS NOT NULL THEN (CASE WHEN [IsDeleted] = @IsDeleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
