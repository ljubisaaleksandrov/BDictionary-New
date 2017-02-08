CREATE TABLE [dbo].[QuestionCreator]
(
    [Id]			INT				IDENTITY (1, 1) NOT NULL,
    [UserID]		NVARCHAR (128)	NOT NULL,
	[QuestionID]	INT				NOT NULL

	CONSTRAINT [PK_dbo.QuestionCreator] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuestionCreator_Question] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[Question] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_QuestionCreator_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
)
