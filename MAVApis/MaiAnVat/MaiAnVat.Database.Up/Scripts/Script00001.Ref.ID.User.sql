set identity_insert [Identity].[User] ON


MERGE INTO [Identity].[User] as [Target] 
USING (
	VALUES
	(1, 0, N'system@',1, 0, NULL, N'86e69fc9fa9314555c4cfcbb4c8addcf', N'0829338633', 0, N'system@', N'SYSTEM', N'', CAST(N'2016-06-15 07:45:38.833' AS DateTime), 1, CAST(N'2016-06-15 07:45:38.837' AS DateTime), 1, 0, N'6hf71', 1),
	(2, 0, N'lekimphi1997@gmail.com',1, 0, NULL, N'86e69fc9fa9314555c4cfcbb4c8addcf', N'0829338644', 0, N'KimPhi', N'Kim', N'Phi', CAST(N'2016-06-15 07:45:38.833' AS DateTime), 1, CAST(N'2016-06-15 07:45:38.837' AS DateTime), 1, 0, N'6hf71', 1),
	(3, 0, N'phi.lekim@ncc.asia',1, 0, NULL, N'86e69fc9fa9314555c4cfcbb4c8addcf', N'0829338655', 0, N'KimPhi1', N'Kim', N'Phi', CAST(N'2016-06-15 07:45:38.833' AS DateTime), 1, CAST(N'2016-06-15 07:45:38.837' AS DateTime), 1, 0, N'6hf71', 1),
	(4, 0, N'van.vuthi@ncc.asia',1, 0, NULL, N'86e69fc9fa9314555c4cfcbb4c8addcf', N'0829338677', 0, N'Vu Van', N'Vu', N'Van', CAST(N'2016-06-15 07:45:38.833' AS DateTime), 1, CAST(N'2016-06-15 07:45:38.837' AS DateTime), 1, 0, N'6hf71', 1),
	(5, 0, N'duy.nguyenxuan@ncc.asia',1, 0, NULL, N'86e69fc9fa9314555c4cfcbb4c8addcf', N'0829338688', 0, N'NguyenDuy', N'Nguyen', N'Xuan Duy', CAST(N'2016-06-15 07:45:38.833' AS DateTime), 1, CAST(N'2016-06-15 07:45:38.837' AS DateTime), 1, 0, N'6hf71', 1)
)
AS [Source] (
	[Id], [AccessFailedCount], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEndDateUtc], 
	[PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [UserName],
	[FirstName], [LastName], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], 
	[ModifiedByUserFK], [IsDeleted], [PasswordSalt], [Status]
) ON [Target].[Id] = [Source].[Id]

WHEN MATCHED THEN
	UPDATE SET
		[AccessFailedCount] = [Source].[AccessFailedCount],
		[Email] = [Source].[Email],
		[EmailConfirmed] = [Source].[EmailConfirmed],
		[LockoutEnabled] = [Source].[LockoutEnabled],
		[LockoutEndDateUtc] = [Source].[LockoutEndDateUtc],
		[PasswordHash] = [Source].[PasswordHash],
		[PhoneNumber] = [Source].[PhoneNumber],
		[PhoneNumberConfirmed] = [Source].[PhoneNumberConfirmed],
		[UserName] = [Source].[UserName],
		[FirstName] = [Source].[FirstName],
		[LastName] = [Source].[LastName],
		[CreatedAtUtc] = [Source].[CreatedAtUtc],
		[CreatedByUserFK] = [Source].[CreatedByUserFK],
		[ModifiedAtUtc] = [Source].[ModifiedAtUtc],
		[ModifiedByUserFK] = [Source].[ModifiedByUserFK],
		[IsDeleted] = [Source].[IsDeleted],
		[PasswordSalt] = [Source].[PasswordSalt],
		[Status] =  [Source].[Status]

WHEN NOT MATCHED BY TARGET THEN
	INSERT (
		[Id], [AccessFailedCount], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEndDateUtc], 
		[PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [UserName], [FirstName], [LastName], [CreatedAtUtc], 
		[CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted],[PasswordSalt], [Status]
	) VALUES (
		[Source].[Id],
		[Source].[AccessFailedCount],
		[Source].[Email],
		[Source].[EmailConfirmed],
		[Source].[LockoutEnabled],
		[Source].[LockoutEndDateUtc],
		[Source].[PasswordHash],
		[Source].[PhoneNumber],
		[Source].[PhoneNumberConfirmed],
		[Source].[UserName],
		[Source].[FirstName],
		[Source].[LastName],
		[Source].[CreatedAtUtc],
		[Source].[CreatedByUserFK],
		[Source].[ModifiedAtUtc],
		[Source].[ModifiedByUserFK],
		[Source].[IsDeleted],
		[Source].[PasswordSalt],
		[Source].[Status])
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;

set identity_insert [Identity].[User] OFF