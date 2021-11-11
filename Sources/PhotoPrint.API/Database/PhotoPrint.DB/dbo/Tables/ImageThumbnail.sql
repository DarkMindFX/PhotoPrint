CREATE TABLE [dbo].[ImageThumbnail] (
    [ID]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [Url]     NVARCHAR (1000) NOT NULL,
    [Order]   INT             NULL,
    [ImageID] BIGINT          NOT NULL,
    CONSTRAINT [PK_ImageThumbnail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ImageThumbnail_Image] FOREIGN KEY ([ImageID]) REFERENCES [dbo].[Image] ([ID]) ON DELETE CASCADE
);

