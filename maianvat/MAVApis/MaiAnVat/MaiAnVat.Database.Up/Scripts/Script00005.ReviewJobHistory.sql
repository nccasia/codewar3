USE [MaiAnVat]
GO
/****** Object:  Table [dbo].[RejectedReason]    Script Date: 7/23/2021 10:18:06 PM ******/
IF OBJECT_ID('dbo.RejectedReason') IS NULL
BEGIN
	CREATE TABLE [dbo].[RejectedReason](
		[RejectedReasonK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
		[Identity] [int] IDENTITY(1,1) NOT NULL,
		[ReasonFK] [uniqueidentifier] NOT NULL,
		[ReviewJobHistoryFK] [uniqueidentifier] NOT NULL,
		[CreatedAtUtc] [datetime] NOT NULL,
		[CreatedByUserFK] [int] NOT NULL,
		[ModifiedAtUtc] [datetime] NULL,
		[ModifiedByUserFK] [int] NULL,
		[IsDeleted] [bit] NOT NULL,
	 CONSTRAINT [PK__RejectedReason__9E749C2A838D7FE7] PRIMARY KEY NONCLUSTERED 
	(
		[RejectedReasonK] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ReviewJobHistory]    Script Date: 7/23/2021 10:18:06 PM ******/
IF OBJECT_ID('dbo.ReviewJobHistory') IS NULL
BEGIN
	
	CREATE TABLE [dbo].[ReviewJobHistory](
		[ReviewJobHistoryK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
		[Identity] [int] IDENTITY(1,1) NOT NULL,
		[ReviewStatusFK] [uniqueidentifier] NOT NULL,
		[CreatedAtUtc] [datetime] NOT NULL,
		[CreatedByUserFK] [int] NOT NULL,
		[ModifiedAtUtc] [datetime] NULL,
		[ModifiedByUserFK] [int] NULL,
		[IsDeleted] [bit] NOT NULL,
		[ReviewBy] [int] NOT NULL,
		[ReviewTimeStamp] [datetime] NULL,
	 CONSTRAINT [PK__ReviewJobHistory__9E749C2A838D7FE7] PRIMARY KEY NONCLUSTERED 
	(
		[ReviewJobHistoryK] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	ALTER TABLE [dbo].[RejectedReason] ADD  CONSTRAINT [DF__RejectedReason__RejectedReasonK__66603565]  DEFAULT (newsequentialid()) FOR [RejectedReasonK]
	ALTER TABLE [dbo].[ReviewJobHistory] ADD  CONSTRAINT [DF__ReviewJobHistory__ReviewJobHistoryK__66603565]  DEFAULT (newsequentialid()) FOR [ReviewJobHistoryK]
	ALTER TABLE [dbo].[RejectedReason]  WITH NOCHECK ADD  CONSTRAINT [jobs.RejectedReason_ReasonFK.ListCategory_ListCategoryK] FOREIGN KEY([ReasonFK])
	REFERENCES [dbo].[ListCategory] ([ListCategoryK])
	ALTER TABLE [dbo].[RejectedReason] CHECK CONSTRAINT [jobs.RejectedReason_ReasonFK.ListCategory_ListCategoryK]
	ALTER TABLE [dbo].[RejectedReason]  WITH NOCHECK ADD  CONSTRAINT [jobs.RejectedReason_ReviewJobHistoryFK.ReviewJobHistory_ReviewJobHistoryK] FOREIGN KEY([ReviewJobHistoryFK])
	REFERENCES [dbo].[ReviewJobHistory] ([ReviewJobHistoryK])
	ALTER TABLE [dbo].[RejectedReason] CHECK CONSTRAINT [jobs.RejectedReason_ReviewJobHistoryFK.ReviewJobHistory_ReviewJobHistoryK]
	ALTER TABLE [dbo].[ReviewJobHistory]  WITH NOCHECK ADD  CONSTRAINT [jobs.ReviewJobHistory_ReviewStatusFK.ListCategory_ListCategoryK] FOREIGN KEY([ReviewStatusFK])
	REFERENCES [dbo].[ListCategory] ([ListCategoryK])
	ALTER TABLE [dbo].[ReviewJobHistory] CHECK CONSTRAINT [jobs.ReviewJobHistory_ReviewStatusFK.ListCategory_ListCategoryK]
END
