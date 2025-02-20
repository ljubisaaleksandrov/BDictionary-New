﻿/*
Deployment script for BDictionary

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "BDictionary"
:setvar DefaultFilePrefix "BDictionary"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Creating [dbo].[Question]...';


GO
CREATE TABLE [dbo].[Question] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Value]      NVARCHAR (256) NOT NULL,
    [AnswerID]   INT            NOT NULL,
    [CategoryID] INT            NOT NULL,
    CONSTRAINT [PK_dbo.Question] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[QuestionAnswer]...';


GO
CREATE TABLE [dbo].[QuestionAnswer] (
    [Id]                        INT            IDENTITY (1, 1) NOT NULL,
    [Value]                     NVARCHAR (256) NOT NULL,
    [CategoryID]                INT            NOT NULL,
    [QuestionAnswerValueTypeID] INT            NOT NULL,
    CONSTRAINT [PK_dbo.QuestionAnswer] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[QuestionAnswerValueType]...';


GO
CREATE TABLE [dbo].[QuestionAnswerValueType] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Value] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_dbo.QuestionAnswerValueType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[QuestionCategory]...';


GO
CREATE TABLE [dbo].[QuestionCategory] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (256) NOT NULL,
    [ParentID] INT            NOT NULL,
    CONSTRAINT [PK_dbo.QuestionCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Question_Answer]...';


GO
ALTER TABLE [dbo].[Question] WITH NOCHECK
    ADD CONSTRAINT [FK_Question_Answer] FOREIGN KEY ([AnswerID]) REFERENCES [dbo].[QuestionAnswer] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Question_QuestionCategory]...';


GO
ALTER TABLE [dbo].[Question] WITH NOCHECK
    ADD CONSTRAINT [FK_Question_QuestionCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[QuestionCategory] ([Id]);


GO
PRINT N'Creating [dbo].[FK_QuestionAnswer_QuestionCategory]...';


GO
ALTER TABLE [dbo].[QuestionAnswer] WITH NOCHECK
    ADD CONSTRAINT [FK_QuestionAnswer_QuestionCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[QuestionCategory] ([Id]);


GO
PRINT N'Creating [dbo].[FK_QuestionAnswer_QuestionAnswerValueType]...';


GO
ALTER TABLE [dbo].[QuestionAnswer] WITH NOCHECK
    ADD CONSTRAINT [FK_QuestionAnswer_QuestionAnswerValueType] FOREIGN KEY ([QuestionAnswerValueTypeID]) REFERENCES [dbo].[QuestionAnswerValueType] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Question] WITH CHECK CHECK CONSTRAINT [FK_Question_Answer];

ALTER TABLE [dbo].[Question] WITH CHECK CHECK CONSTRAINT [FK_Question_QuestionCategory];

ALTER TABLE [dbo].[QuestionAnswer] WITH CHECK CHECK CONSTRAINT [FK_QuestionAnswer_QuestionCategory];

ALTER TABLE [dbo].[QuestionAnswer] WITH CHECK CHECK CONSTRAINT [FK_QuestionAnswer_QuestionAnswerValueType];


GO
PRINT N'Update complete.';


GO
