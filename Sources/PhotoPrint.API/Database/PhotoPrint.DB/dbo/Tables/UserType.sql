CREATE TABLE [dbo].[UserType] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserTypeName] NVARCHAR (50) NOT NULL,
    [IsDeleted]    BIT           CONSTRAINT [DF_UserType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

