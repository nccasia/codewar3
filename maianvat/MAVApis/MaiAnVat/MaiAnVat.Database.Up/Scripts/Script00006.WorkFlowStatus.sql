DECLARE @categoryK UNIQUEIDENTIFIER;

IF NOT EXISTS (select 1 from [dbo].[Category] where [Name] = N'JobReviewStatus')
BEGIN
Set @categoryK = newid()
INSERT INTO [dbo].[Category] ([CategoryK], [Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted])
Values(@categoryK, N'JobReviewStatus', N'Trạng thái phê duyệt công việc', GetDate(), 1, GetDate(), 1, 0)
END
ELSE
BEGIN
SELECT @categoryK = [CategoryK] from [dbo].[Category]
where [Name] = N'JobReviewStatus'
END
MERGE INTO [dbo].[ListCategory] as [Target]
USING(
VALUES
(NEWID(),N'Awaiting Review',@categoryK,GETUTCDATE(),1,0,N'Đang chờ phê duyệt',0),
(NEWID(),N'Declined',@categoryK,GETUTCDATE(),1,0,N'Đã từ chối',0),
(NEWID(),N'Approved',@categoryK,GETUTCDATE(),1,0,N'Đã chấp nhận',0)
)
AS [Source] (
[ListCategoryK],
[Name],
[CategoryFK],
[CreatedAtUtc],
[CreatedByUserFK],
[IsDeleted],
[Description],
[IsDisabled]
) ON [Target].[Name] = [Source].[Name] AND [Target].[CategoryFK] = [Source].[CategoryFK]

WHEN MATCHED THEN
UPDATE SET
[Description] = [Source].[Description],
[IsDisabled] = [Source].[IsDisabled],
[IsDeleted] = [Source].[IsDeleted]

WHEN NOT MATCHED BY TARGET THEN
INSERT (
[ListCategoryK],
[Name],
[CategoryFK],
[CreatedAtUtc],
[CreatedByUserFK],
[IsDeleted],
[Description],
[IsDisabled]
) VALUES (
[Source].[ListCategoryK],
[Source].[Name],
[Source].[CategoryFK],
[Source].[CreatedAtUtc],
[Source].[CreatedByUserFK],
[Source].[IsDeleted],
[Source].[Description],
[Source].[IsDisabled]
);

GO

DECLARE @categoryK UNIQUEIDENTIFIER;

--Insert 'JobRejectedReason' into Category if not exists
IF NOT EXISTS (select 1 from [dbo].[Category] where [Name] = N'JobRejectedReason')
BEGIN
Set @categoryK = newid()
INSERT INTO [dbo].[Category] ([CategoryK], [Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted])
Values(@categoryK, N'JobRejectedReason', N'Lý do từ chối công việc', GetDate(), 1, GetDate(), 1, 0)
END
ELSE
BEGIN
SELECT @categoryK = [CategoryK] from [dbo].[Category]
where [Name] = N'JobRejectedReason'
END
MERGE INTO [dbo].[ListCategory] as [Target]
USING(
VALUES
(NEWID(),N'Other',@categoryK,GETUTCDATE(),1,0,N'Khác',0),
(NEWID(),N'Rework',@categoryK,GETUTCDATE(),1,0,N'Cần được làm lại',0),
(NEWID(),N'MoreRequirement',@categoryK,GETUTCDATE(),1,0,N'Thêm yêu cầu mới',0)
)
AS [Source] (
[ListCategoryK],
[Name],
[CategoryFK],
[CreatedAtUtc],
[CreatedByUserFK],
[IsDeleted],
[Description],
[IsDisabled]
) ON [Target].[Name] = [Source].[Name] AND [Target].[CategoryFK] = [Source].[CategoryFK]

WHEN MATCHED THEN
UPDATE SET
[Description] = [Source].[Description],
[IsDisabled] = [Source].[IsDisabled],
[IsDeleted] = [Source].[IsDeleted]

WHEN NOT MATCHED BY TARGET THEN
INSERT (
[ListCategoryK],
[Name],
[CategoryFK],
[CreatedAtUtc],
[CreatedByUserFK],
[IsDeleted],
[Description],
[IsDisabled]
) VALUES (
[Source].[ListCategoryK],
[Source].[Name],
[Source].[CategoryFK],
[Source].[CreatedAtUtc],
[Source].[CreatedByUserFK],
[Source].[IsDeleted],
[Source].[Description],
[Source].[IsDisabled]
);

GO

DECLARE @categoryK UNIQUEIDENTIFIER;

IF NOT EXISTS (select 1 from [dbo].[Category] where [Name] = N'JobType')
BEGIN
Set @categoryK = newid()
INSERT INTO [dbo].[Category] ([CategoryK], [Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted])
Values(@categoryK, N'JobType', N'Danh sách loại công việc ', GetDate(), 1, GetDate(), 1, 0)
END
ELSE
BEGIN
SELECT @categoryK = [CategoryK] from [dbo].[Category]
where [Name] = N'JobType'
END
MERGE INTO [dbo].[ListCategory] as [Target]
USING(
VALUES
(NEWID(),N'BA',@categoryK,GETUTCDATE(),1,0,N'Business Analyst',0),
(NEWID(),N'QA',@categoryK,GETUTCDATE(),1,0,N'Quality Assurance',0),
(NEWID(),N'Design',@categoryK,GETUTCDATE(),1,0,N'Thiết kế',0),
(NEWID(),N'Develop',@categoryK,GETUTCDATE(),1,0,N'Phát triển',0)
)
AS [Source] (
[ListCategoryK],
[Name],
[CategoryFK],
[CreatedAtUtc],
[CreatedByUserFK],
[IsDeleted],
[Description],
[IsDisabled]
) ON [Target].[Name] = [Source].[Name] AND [Target].[CategoryFK] = [Source].[CategoryFK]

WHEN MATCHED THEN
UPDATE SET
[Description] = [Source].[Description],
[IsDisabled] = [Source].[IsDisabled],
[IsDeleted] = [Source].[IsDeleted]

WHEN NOT MATCHED BY TARGET THEN
INSERT (
[ListCategoryK],
[Name],
[CategoryFK],
[CreatedAtUtc],
[CreatedByUserFK],
[IsDeleted],
[Description],
[IsDisabled]
) VALUES (
[Source].[ListCategoryK],
[Source].[Name],
[Source].[CategoryFK],
[Source].[CreatedAtUtc],
[Source].[CreatedByUserFK],
[Source].[IsDeleted],
[Source].[Description],
[Source].[IsDisabled]
);
GO