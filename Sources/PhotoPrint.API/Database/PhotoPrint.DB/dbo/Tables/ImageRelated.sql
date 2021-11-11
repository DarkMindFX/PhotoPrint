CREATE TABLE [dbo].[ImageRelated] (
    [ImageID]        BIGINT NOT NULL,
    [RelatedImageID] BIGINT NOT NULL,
    CONSTRAINT [PK_ImageRelated_1] PRIMARY KEY CLUSTERED ([ImageID] ASC, [RelatedImageID] ASC),
    CONSTRAINT [FK_ImageRelated_RelatedImage] FOREIGN KEY ([RelatedImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ImageRelated_RootImage] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID])
);

