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
