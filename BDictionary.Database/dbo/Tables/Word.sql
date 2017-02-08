CREATE TABLE [dbo].[Word]
(
    [Id]			INT				IDENTITY (1, 1) NOT NULL,
    [Value]			NVARCHAR (30)	NOT NULL,
	[Description]	NVARCHAR (max)	NOT NULL,
	[Example]		NVARCHAR (max)	NOT NULL,
	[TypeID]		INT				NULL,

	CONSTRAINT [PK_dbo.Word] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Word_WordType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[WordType] ([Id])
)

