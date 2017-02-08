CREATE TABLE [dbo].[Question]
(
    [Id]			INT				IDENTITY (1, 1) NOT NULL,
    [Value]			NVARCHAR (max)	NOT NULL,
	[AnswerID]		INT				NOT NULL,
	[IsShiz]		BIT				NOT NULL,
	[Creator]		NVARCHAR (256)	NOT NULL,
	[CategoryID]	INT				NOT NULL,
	[DateCreated]	DATETIME2		NOT NULL

	CONSTRAINT [PK_dbo.Question] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Question_Answer] FOREIGN KEY ([AnswerID]) REFERENCES [dbo].[QuestionAnswer] ([Id]) ON DELETE CASCADE
    CONSTRAINT [FK_Question_QuestionCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[QuestionCategory] ([Id])
)
