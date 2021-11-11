CREATE TABLE [dbo].[ImageCategory] (
    [ImageID]    BIGINT NOT NULL,
    [CategoryID] BIGINT NOT NULL,
    CONSTRAINT [PK_ImageCategory_1] PRIMARY KEY CLUSTERED ([ImageID] ASC, [CategoryID] ASC),
    CONSTRAINT [FK_ImageCategory_Category] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Category] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ImageCategory_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

