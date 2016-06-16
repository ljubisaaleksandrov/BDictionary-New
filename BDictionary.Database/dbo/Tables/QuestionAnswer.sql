CREATE TABLE [dbo].[QuestionAnswer]
(
    [Id]						INT				IDENTITY (1, 1) NOT NULL,
    [Value]						NVARCHAR (256)	NOT NULL,
	[CategoryID]				INT				NOT NULL,
	[IsPrimary]					Bit				NOT NULL

	CONSTRAINT [PK_dbo.QuestionAnswer] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_QuestionAnswer_QuestionCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[QuestionCategory] ([Id])
)
