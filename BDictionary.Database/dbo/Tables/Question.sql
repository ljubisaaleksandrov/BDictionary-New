CREATE TABLE [dbo].[Question]
(
    [Id]			INT				IDENTITY (1, 1) NOT NULL,
    [Value]			NVARCHAR (256)	NOT NULL,
	[AnswerID]		INT				NOT NULL,
	[CategoryID]	INT				NOT NULL

	CONSTRAINT [PK_dbo.Question] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Question_Answer] FOREIGN KEY ([AnswerID]) REFERENCES [dbo].[QuestionAnswer] ([Id])
    CONSTRAINT [FK_Question_QuestionCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[QuestionCategory] ([Id])
)
