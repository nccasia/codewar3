
---Category
MERGE INTO [dbo].[Category] as [Target] 
USING (
	VALUES
	(N'Loại công việc',N'Danh sách loại công việc '),
	(N'Loại hành động',N'Loại hành động'),
	(N'Loại trạng thái',N'Loại trạng thái'),
	(N'Loại lịch trình',N'Loại lịch trình'),
	(N'Trạng thái công việc',N'Trạng thái công việc')
)
AS [Source] (
	[Name], [Description]
	) 
	ON [Target].[Name] = [Source].[Name]

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
