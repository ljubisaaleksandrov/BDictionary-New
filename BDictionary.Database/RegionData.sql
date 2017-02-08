SET IDENTITY_INSERT [dbo].[Region] ON;
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
(20, N'Paјчиловци', N''),
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
UPDATE SET  Name = Source.Name, [Description] = Source.[Description]
-- insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Name, [Description]) 
VALUES (Id, Name, [Description]);
-- delete rows that are in the target but not the source
-- WHEN NOT MATCHED BY SOURCE THEN
-- DELETE
GO
SET IDENTITY_INSERT [dbo].[Region] OFF;
GO