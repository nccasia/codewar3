set identity_insert [Identity].[User] ON


MERGE INTO [Identity].[User] as [Target] 
USING (
	VALUES
	(1, 0, N'system@',1, 0, NULL, N'86e69fc9fa9314555c4cfcbb4c8addcf', NULL, 0, N'system@', N'SYSTEM', N'', CAST(N'2016-06-15 07:45:38.833' AS DateTime), 1, CAST(N'2016-06-15 07:45:38.837' AS DateTime), 1, 0, N'6hf71')
)
AS [Source] (
	[Id], [AccessFailedCount], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEndDateUtc], 
	[PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [UserName],
	[FirstName], [LastName], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], 
	[ModifiedByUserFK], [IsDeleted], [PasswordSalt]
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
		[PasswordSalt] = [Source].[PasswordSalt]

WHEN NOT MATCHED BY TARGET THEN
	INSERT (
		[Id], [AccessFailedCount], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEndDateUtc], 
		[PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [UserName], [FirstName], [LastName], [CreatedAtUtc], 
		[CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted],[PasswordSalt]
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
		[Source].[PasswordSalt])
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;

set identity_insert [Identity].[User] OFF