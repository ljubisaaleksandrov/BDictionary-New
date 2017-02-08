CREATE TABLE [dbo].[Region]
(
    [Id]			INT				IDENTITY (1, 1) NOT NULL,
    [Name]			NVARCHAR (30)	NOT NULL,
	[Description]	NVARCHAR (max)	NOT NULL,

	CONSTRAINT [PK_dbo.Region] PRIMARY KEY CLUSTERED ([Id] ASC)
)
