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
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE Latin1_General_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[AspNetRoles]...';


GO
CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetUserClaims]...';


GO
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetUserLogins]...';


GO
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetUserRoles]...';


GO
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetUsers]...';


GO
CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Question]...';


GO
CREATE TABLE [dbo].[Question] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Value]       NVARCHAR (MAX) NOT NULL,
    [AnswerID]    INT            NOT NULL,
    [IsShiz]      BIT            NOT NULL,
    [Creator]     NVARCHAR (256) NOT NULL,
    [CategoryID]  INT            NOT NULL,
    [DateCreated] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_dbo.Question] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[QuestionAnswer]...';


GO
CREATE TABLE [dbo].[QuestionAnswer] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Value]      NVARCHAR (256) NOT NULL,
    [CategoryID] INT            NOT NULL,
    [IsPrimary]  BIT            NOT NULL,
    CONSTRAINT [PK_dbo.QuestionAnswer] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[QuestionCategory]...';


GO
CREATE TABLE [dbo].[QuestionCategory] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (256) NOT NULL,
    [AnswerType] INT            NOT NULL,
    [ParentID]   INT            NULL,
    CONSTRAINT [PK_dbo.QuestionCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Region]...';


GO
CREATE TABLE [dbo].[Region] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (30)  NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL
);


GO
PRINT N'Creating [dbo].[Synonym]...';


GO
CREATE TABLE [dbo].[Synonym] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [FirstSynonymID]  INT NOT NULL,
    [SecondSynonymID] INT NOT NULL,
    CONSTRAINT [PK_dbo.Synonym] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Word]...';


GO
CREATE TABLE [dbo].[Word] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Value]       NVARCHAR (30)  NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Example]     NVARCHAR (MAX) NOT NULL,
    [TypeID]      INT            NULL,
    CONSTRAINT [PK_dbo.Word] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[WordType]...';


GO
CREATE TABLE [dbo].[WordType] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Value] NVARCHAR (30) NOT NULL,
    CONSTRAINT [PK_dbo.WordType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserClaims]
    ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserLogins]
    ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]...';


GO
ALTER TABLE [dbo].[AspNetUserRoles]
    ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserRoles]
    ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Question_Answer]...';


GO
ALTER TABLE [dbo].[Question]
    ADD CONSTRAINT [FK_Question_Answer] FOREIGN KEY ([AnswerID]) REFERENCES [dbo].[QuestionAnswer] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Question_QuestionCategory]...';


GO
ALTER TABLE [dbo].[Question]
    ADD CONSTRAINT [FK_Question_QuestionCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[QuestionCategory] ([Id]);


GO
PRINT N'Creating [dbo].[FK_QuestionAnswer_QuestionCategory]...';


GO
ALTER TABLE [dbo].[QuestionAnswer]
    ADD CONSTRAINT [FK_QuestionAnswer_QuestionCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[QuestionCategory] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_QuestionCategory_ParentQuestionCategory]...';


GO
ALTER TABLE [dbo].[QuestionCategory]
    ADD CONSTRAINT [FK_QuestionCategory_ParentQuestionCategory] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[QuestionCategory] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Word_FirstSynonym]...';


GO
ALTER TABLE [dbo].[Synonym]
    ADD CONSTRAINT [FK_Word_FirstSynonym] FOREIGN KEY ([FirstSynonymID]) REFERENCES [dbo].[Word] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Word_SecondSynonym]...';


GO
ALTER TABLE [dbo].[Synonym]
    ADD CONSTRAINT [FK_Word_SecondSynonym] FOREIGN KEY ([SecondSynonymID]) REFERENCES [dbo].[Word] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Word_WordType]...';


GO
ALTER TABLE [dbo].[Word]
    ADD CONSTRAINT [FK_Word_WordType] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[WordType] ([Id]);


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'f56c6642-82a9-4f74-ab34-4dc6e190de68')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('f56c6642-82a9-4f74-ab34-4dc6e190de68')

GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

----------------------------- AspNetRoles -----------------------------
GO

MERGE INTO AspNetRoles AS Target 
USING (VALUES 
(N'69c29dc2-d5f8-41f5-aca8-5147216569ee', N'Admin'),
(N'057e1709-59f1-4ada-9464-362a72ad9999', N'User')) 
AS Source ([Id], [Name]) 
ON Target.Id = Source.Id
-- update matched rows 
WHEN MATCHED THEN 
UPDATE SET Id = Source.Id, Name = Source.Name
-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Name) 
VALUES (Id, Name);
-- delete rows that are in the target but not the source
-- WHEN NOT MATCHED BY SOURCE THEN
-- DELETE
GO




----------------------------- AspNetUsers -----------------------------

MERGE INTO AspNetUsers AS Target 
USING (VALUES (N'c98bbf5f-5657-4fb8-b205-772500ab9863'
			,N'admin@bos.com'
			,1
			,N'ACTYzBTj1pryj9Dx+4bRmZGo1MaeLxRcueQEBC/uU4ER9Vx7MSKpJF936pRjn5wK9A=='	--	B0$!legrad
			,N'078753f5-b3d3-43b4-81f8-5889387e2764'
			,NULL
			,0
			,0
			,NULL
			,1
			,0
			,N'admin@bos.com'))
AS Source (	 [Id]
			,[Email]
			,[EmailConfirmed]
			,[PasswordHash]
			,[SecurityStamp]
			,[PhoneNumber]
			,[PhoneNumberConfirmed]
			,[TwoFactorEnabled]
			,[LockoutEndDateUtc]
			,[LockoutEnabled]
			,[AccessFailedCount]
			,[UserName]) 
ON Target.Id = Source.Id
-- update matched rows 
WHEN MATCHED THEN 
UPDATE SET	Id = Source.Id, 
			Email = Source.Email, 
			EmailConfirmed = Source.EmailConfirmed,
			PasswordHash = Source.PasswordHash,
			SecurityStamp = Source.SecurityStamp,
			PhoneNumber = Source.PhoneNumber,
			PhoneNumberConfirmed = Source.PhoneNumberConfirmed,
			TwoFactorEnabled = Source.TwoFactorEnabled,
			LockoutEndDateUtc = Source.LockoutEndDateUtc,
			LockoutEnabled = Source.LockoutEnabled,
			AccessFailedCount = Source.AccessFailedCount,
			UserName = Source.UserName
-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ( Id
		,Email
		,EmailConfirmed
		,PasswordHash
		,SecurityStamp
		,PhoneNumber
		,PhoneNumberConfirmed
		,TwoFactorEnabled
		,LockoutEndDateUtc
		,LockoutEnabled
		,AccessFailedCount
		,UserName) 
VALUES (Id
		,Email
		,EmailConfirmed
		,PasswordHash
		,SecurityStamp
		,PhoneNumber
		,PhoneNumberConfirmed
		,TwoFactorEnabled
		,LockoutEndDateUtc
		,LockoutEnabled
		,AccessFailedCount
		,UserName);
-- delete rows that are in the target but not the source
-- WHEN NOT MATCHED BY SOURCE THEN
-- DELETE
GO





----------------------------- AspNetUserRoles -----------------------------

MERGE INTO AspNetUserRoles AS Target 
USING (VALUES (N'c98bbf5f-5657-4fb8-b205-772500ab9863', N'69c29dc2-d5f8-41f5-aca8-5147216569ee')) 
AS Source ([UserId], [RoleId]) 
ON Target.UserId = Source.UserId
-- update matched rows 
WHEN MATCHED THEN 
UPDATE SET UserId = Source.UserId, RoleId = Source.RoleId
-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (UserId, RoleId) 
VALUES (UserId, RoleId);
-- delete rows that are in the target but not the source
-- WHEN NOT MATCHED BY SOURCE THEN
-- DELETE
GO

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
