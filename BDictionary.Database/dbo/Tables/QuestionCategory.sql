CREATE TABLE [dbo].[QuestionCategory]
(
    [Id]			INT				IDENTITY (1, 1) NOT NULL,
    [Name]			NVARCHAR (256)	NOT NULL,
    [AnswerType]	INT				NOT NULL,
	[ParentID]		INT				NULL,
	[DateCreated]	DATETIME2		NOT NULL,

    CONSTRAINT [PK_dbo.QuestionCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuestionCategory_ParentQuestionCategory] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[QuestionCategory] ([Id])
)
