DELETE FROM [dbo].[Permission]
GO

set identity_insert [dbo].[Permission] OFF

MERGE INTO [dbo].[Permission] as [Target] 
USING (
	VALUES
	(N'a321e343-34fb-4890-ac9d-df3b5a242980', N'Administration', N'Change User Status', N'Thay đổi trạng thái người dùng ', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'0c2ee4a4-1e72-4691-89a0-6a6941812d09', N'Administration', N'Add/Edit User Details', N'Thêm/sửa chi tiết người dùng', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'ae782b6e-697f-49c0-a97f-d775c3ab055e', N'Administration', N'Delete/Undelete User', N'Xóa/Hủy xóa người dùng', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'd47d38e8-126a-4793-a16e-ca5c207147ef', N'Administration', N'Updates Administration Settings', N'Toàn bộ quyền quản lý người dùng', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'11e8b6aa-7e26-457e-9126-cf9057c54202', N'Jobs', N'Allow Manual Job Entry', N'Cho phép Nhập công việc Thủ công', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'f7218c68-b237-445d-bfcb-8d191d1934de', N'Jobs', N'Deactivate Job', N'Cho phép người dùng hủy kích hoạt công việc', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'aa6d5a85-1ae5-4700-ab7b-14fc11e5d123', N'Jobs', N'Delete Job', N'Cho phép người dùng xóa công việc', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'2540f9a5-ec3f-4d59-9dc4-9eb134abc812', N'Jobs', N'Edit Job', N'Cho phép người dùng chỉnh sửa công việc', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'2540f9a5-ec3f-4d59-9dc4-9eb134abc813', N'Jobs', N'Add Job', N'Cho phép người dùng chỉnh thêm công việc', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'2540f9a5-ec3f-4d59-9dc4-9eb134abc814', N'Job registration', N'Job registration approval', N'Cho phép người dùng phê duyệt đơn đăng ký công việc', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'2540f9a5-ec3f-4d59-9dc4-9eb134abc815', N'Job registration', N'Job registration approval', N'Cho phép người dùng phê duyệt đơn đăng ký công việc', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'2540f9a5-ec3f-4d59-9dc4-9eb134abc816', N'Work approvals', N'Work approvals - readonly', N'Cho phép người dùng xem đơn gửi kiểm tra hoàn thành công việc', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0),
	(N'2540f9a5-ec3f-4d59-9dc4-9eb134abc817', N'Work approvals', N'Work approvals - full access', N'Cho phép người dùng sử dụng toàn bộ chức năng kiểm tra hoàn thành công việc', 1, CAST(N'2021-07-23 07:45:38.833' AS DateTime), 1, CAST(N'2021-07-23 07:45:38.837' AS DateTime), 1, 0)
)
AS [Source] (
	[PermissionK], [GroupingName], [Name], [Description], [IsEnabled], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted]
) ON [Target].[PermissionK] = [Source].[PermissionK]

WHEN MATCHED THEN
	UPDATE SET
		[GroupingName] = [Source].[GroupingName],
		[Name] = [Source].[Name],
		[Description] = [Source].[Description],
		[IsEnabled] = [Source].[IsEnabled],
		[CreatedAtUtc] = [Source].[CreatedAtUtc],
		[CreatedByUserFK] = [Source].[CreatedByUserFK],
		[ModifiedAtUtc] = [Source].[ModifiedAtUtc],
		[ModifiedByUserFK] = [Source].[ModifiedByUserFK],
		[IsDeleted] = [Source].[IsDeleted]

WHEN NOT MATCHED BY TARGET THEN
	INSERT (
		[PermissionK], [GroupingName], [Name], [Description], [IsEnabled], 
		[CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], 
		[ModifiedByUserFK], [IsDeleted]
	) VALUES (
		[Source].[PermissionK],
		[Source].[GroupingName],
		[Source].[Name],
		[Source].[Description],
		[Source].[IsEnabled],
		[Source].[CreatedAtUtc],
		[Source].[CreatedByUserFK],
		[Source].[ModifiedAtUtc],
		[Source].[ModifiedByUserFK],
		[Source].[IsDeleted])
WHEN NOT MATCHED BY SOURCE THEN 
	DELETE;

set identity_insert [dbo].[Permission] ON
