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

----------------------------- AspNetRoles -----------------------------
GO

MERGE INTO Region AS Target 
USING (VALUES 
(1, N'Голеш', N''),
(2, N'Жеравино', N''),
(3, N'Горњо Тлмино', N''),
(4, N'Караманица', N''),
(5, N'Доганица', N''),
(6, N'Назрица', N''),
(7, N'Долњо Тлмино', N''),
(8, N'Бистар', N''),
(9, N'Јарешник', N''),
(10, N'Рикачево', N''),
(11, N'Бранковци', N''),
(12, N'Зли Дол', N''),
(13, N'Рибарци', N''),
(14, N'Ресен', N''),
(15, N'Млекоминци', N''),
(16, N'Бресница', N''),
(17, N'Буцаљево', N''),
(18, N'Радичевци', N''),
(19, N'Паралово', N''),
(20, N'Парјчиловци', N''),
(21, N'Босилеград', N''),
(22, N'Белут', N''),
(23, N'Извор', N''),
(24, N'Грунци', N''),
(25, N'Долња Лисина', N''),
(26, N'Црноштица', N''),
(27, N'Дукат', N''),
(28, N'Долња Љубата', N''),
(29, N'Милевци', N''),
(30, N'Мусуљ', N''),
(31, N'Горња Љубата', N''),
(32, N'Гложје', N''),
(33, N'Плоча', N''),
(34, N'Барје', N''),
(35, N'Горња Лисина', N''),
(36, N'Долња Ржана', N''),
(37, N'Горња Ржана', N'')) 
AS Source ([Id], [Name], [Description]) 
ON Target.Id = Source.Id
-- update matched rows 
WHEN MATCHED THEN 
UPDATE SET Id = Source.Id, Name = Source.Name, Description = Source.Description
-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Name, Description) 
VALUES (Id, Name, Description);
-- delete rows that are in the target but not the source
-- WHEN NOT MATCHED BY SOURCE THEN
-- DELETE
GO

GO
PRINT N'Update complete.';


GO
