CREATE TABLE [dbo].[Synonym]
(
    [Id]				INT		IDENTITY (1, 1) NOT NULL,
    [FirstSynonymID]	INT		NOT NULL,
    [SecondSynonymID]	INT		NOT NULL,

	CONSTRAINT [PK_dbo.Synonym] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Word_FirstSynonym] FOREIGN KEY ([FirstSynonymID]) REFERENCES [dbo].[Word] ([Id]),
    CONSTRAINT [FK_Word_SecondSynonym] FOREIGN KEY ([SecondSynonymID]) REFERENCES [dbo].[Word] ([Id])
)
