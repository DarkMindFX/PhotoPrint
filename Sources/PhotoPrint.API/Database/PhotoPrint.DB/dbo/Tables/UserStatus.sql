CREATE TABLE [dbo].[UserStatus] (
    [ID]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [StatusName] NVARCHAR (50) NOT NULL,
    [IsDeleted]  BIT           CONSTRAINT [DF_UserStatus_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED ([ID] ASC)
);

