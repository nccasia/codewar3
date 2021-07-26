
---Category
MERGE INTO [dbo].[Category] as [Target] 
USING (
	VALUES
	(N'JobType',N'Danh sách loại công việc '),
	(N'ActionType',N'Loại hành động'),
	(N'StatusType',N'Loại trạng thái'),
	(N'ScheduleType',N'Loại lịch trình')
)
AS [Source] (
	[Name], [Description]
	) 
	ON [Target].[Name] = [Source].[Name] OR [Target].[Description] = [Source].[Description]

WHEN MATCHED THEN
	UPDATE SET 
		[Description] = [Source].[Description]
WHEN NOT MATCHED BY TARGET THEN
INSERT (
	[Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted]
) VALUES (
	[Source].[Name],
	[Source].[Description],
	sysdatetime(),
	1,
	sysdatetime(),
	1,
	0
);
