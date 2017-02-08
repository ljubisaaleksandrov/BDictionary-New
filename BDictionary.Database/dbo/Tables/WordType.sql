CREATE TABLE [dbo].[WordType]
(
    [Id]			INT				IDENTITY (1, 1) NOT NULL,
    [Value]			NVARCHAR (30)	NOT NULL,

	CONSTRAINT [PK_dbo.WordType] PRIMARY KEY CLUSTERED ([Id] ASC)
)