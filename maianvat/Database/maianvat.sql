USE [MaiAnVat]
GO
/****** Object:  User [evolution]    Script Date: 7/18/2021 6:54:32 PM ******/
/****** Object:  Schema [Identity]    Script Date: 7/18/2021 6:54:32 PM ******/
CREATE SCHEMA [Identity]
GO
/****** Object:  Table [dbo].[AccessTokens]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessTokens](
	[AccessTokenId] [int] IDENTITY(1,1) NOT NULL,
	[Token] [char](5000) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[EffectiveTime] [datetimeoffset](7) NOT NULL,
	[ExpiresIn] [int] NOT NULL,
	[UserAgent] [varchar](200) NOT NULL,
	[IP] [varchar](20) NOT NULL,
 CONSTRAINT [PK_AccessTokens_1] PRIMARY KEY CLUSTERED 
(
	[AccessTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Editable] [bit] NULL,
	[Description] [nvarchar](2024) NULL,
	[IsDisabled] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryClassification]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryClassification](
	[CategoryClassificationK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](512) NOT NULL,
	[CategoryFK] [uniqueidentifier] NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Description] [nvarchar](2024) NULL,
	[IsDisabled] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileStorage]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileStorage](
	[FileStorageK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[FileFolder] [nvarchar](255) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_FileStorage] PRIMARY KEY CLUSTERED 
(
	[FileStorageK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[GroupK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[Status] [int] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[ParentGroupFK] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupPermission]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPermission](
	[GroupPermissionK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[GroupFK] [uniqueidentifier] NULL,
	[PermissionFK] [uniqueidentifier] NULL,
	[Status] [int] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[JobK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[JobStatusFK] [uniqueidentifier] NOT NULL,
	[JobTypeFK] [uniqueidentifier] NOT NULL,
	[IsActivated] [bit] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeactivatedUtc] [datetime] NULL,
	[DeactivatedUserFK] [int] NULL,
	[WorkflowStatusFK] [uniqueidentifier] NOT NULL,
	[CustomerOrder] [varchar](50) NULL,
	[Title] [nvarchar](64) NULL,
	[Name] [nvarchar](128) NULL,
	[Description] [nvarchar](512) NULL,
	[RegistrationDeadline] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobAssignedUser]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobAssignedUser](
	[JobAssignedUserK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[JobFK] [uniqueidentifier] NOT NULL,
	[UserFK] [int] NOT NULL,
	[JobAssignmentListFK] [uniqueidentifier] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_JobAssignedUser] PRIMARY KEY CLUSTERED 
(
	[JobAssignedUserK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobAssignmentList]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobAssignmentList](
	[JobAssignmentListK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[GroupFK] [uniqueidentifier] NOT NULL,
	[JobTypeFK] [uniqueidentifier] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_JobAssignmentList] PRIMARY KEY CLUSTERED 
(
	[JobAssignmentListK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobAssignmentListStatus]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobAssignmentListStatus](
	[JobAssignmentListStatusK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[JobAssignmentListFK] [uniqueidentifier] NOT NULL,
	[WorkFlowStatusFK] [uniqueidentifier] NOT NULL,
	[IsEditable] [bit] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_JobAssignmentListStatus] PRIMARY KEY CLUSTERED 
(
	[JobAssignmentListStatusK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobAttachment]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobAttachment](
	[JobAttachmentK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[FileStorageFK] [uniqueidentifier] NOT NULL,
	[ListCategoryFK] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](512) NULL,
	[FileDescription] [nvarchar](2024) NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsDisabled] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobMessage]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobMessage](
	[MessageK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[JobFK] [uniqueidentifier] NOT NULL,
	[UserFK] [int] NOT NULL,
	[MessageText] [nvarchar](4000) NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[MessageFK] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobType]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobType](
	[JobTypeK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NULL,
	[Description] [nvarchar](512) NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DefaultTimeInHours] [int] NOT NULL,
	[ColorCode] [varchar](7) NULL,
	[IsException] [bit] NULL,
	[DefaultWorkFlowFK] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobTypeWorkFlow]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobTypeWorkFlow](
	[JobTypeWorkFlowK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[JobTypeFK] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Description] [nvarchar](512) NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_JobTypeWorkflow] PRIMARY KEY CLUSTERED 
(
	[JobTypeWorkFlowK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobWorkFlowMove]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobWorkFlowMove](
	[JobWorkFlowMoveK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[JobFK] [uniqueidentifier] NOT NULL,
	[FromWorkFlowStatusFK] [uniqueidentifier] NULL,
	[ToWorkFlowStatusFK] [uniqueidentifier] NOT NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_JobWorkFlowMove] PRIMARY KEY CLUSTERED 
(
	[JobWorkFlowMoveK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobWorkFlowStatus]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobWorkFlowStatus](
	[JobWorkFlowStatusK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[JobTypeWorkFlowFK] [uniqueidentifier] NOT NULL,
	[FromWorkFlowStatusFK] [uniqueidentifier] NULL,
	[ToWorkFlowStatusFK] [uniqueidentifier] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_JobWorkFlowStatus] PRIMARY KEY CLUSTERED 
(
	[JobWorkFlowStatusK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListCategory]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListCategory](
	[ListCategoryK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[CategoryFK] [uniqueidentifier] NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Description] [nvarchar](2024) NULL,
	[IsDisabled] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MavMenu]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MavMenu](
	[MavMenuK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](255) NOT NULL,
	[MenuType] [nvarchar](64) NULL,
	[UiSref] [nvarchar](255) NULL,
	[UiSrefActiveIf] [nvarchar](255) NULL,
	[ParentMenuID] [int] NULL,
	[MenuOrder] [tinyint] NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[MenuLevel] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DisplayMenuName] [nvarchar](255) NOT NULL,
	[FeatureKey] [nvarchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[EntityTypeName] [nvarchar](250) NULL,
	[EntityId] [nvarchar](96) NULL,
	[NotificationName] [nvarchar](96) NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[PermissionK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[GroupingName] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[IsEnabled] [bit] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionType]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionType](
	[PermissionTypeK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[OrderOfPrecedence] [int] NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[RefreshTokenK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[UserFK] [int] NULL,
	[IssuedUtc] [datetime] NULL,
	[ExpiresUtc] [datetime] NULL,
	[ProtectedTicket] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistrationJob]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistrationJob](
	[RegistrationJobK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[JobFK] [uniqueidentifier] NOT NULL,
	[IsAccepted] [bit] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[ConfirmedUtc] [datetime] NULL,
	[ConfirmedUserFK] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[ScheduleK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[JobFK] [uniqueidentifier] NOT NULL,
	[ScheduleTypeFK] [uniqueidentifier] NOT NULL,
	[UserFK] [int] NOT NULL,
	[PlannedStartDate] [datetime] NOT NULL,
	[PlannedEndDate] [datetime] NOT NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualEndDate] [datetime] NULL,
	[Lock] [int] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScheduleHistory]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduleHistory](
	[ScheduleHistoryK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleFK] [uniqueidentifier] NOT NULL,
	[Action] [nvarchar](64) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[UserFK] [int] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SchemaVersions]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchemaVersions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScriptName] [nvarchar](255) NOT NULL,
	[Applied] [datetime] NOT NULL,
 CONSTRAINT [PK_SchemaVersions_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[UserGroupK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[GroupFK] [uniqueidentifier] NULL,
	[UserFK] [int] NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginHistory]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginHistory](
	[UserLoginHistoryK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[UserOperation] [nvarchar](50) NULL,
	[Message] [nvarchar](250) NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserNotification]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNotification](
	[UserNotificationK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[UserFK] [int] NOT NULL,
	[NotificationFK] [uniqueidentifier] NOT NULL,
	[State] [int] NOT NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkFlowStatus]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFlowStatus](
	[WorkFlowStatusK] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Identity] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[Description] [varchar](512) NULL,
	[PreviousStatusK] [uniqueidentifier] NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[StatusCode] [varchar](64) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Identity].[Claim]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Identity].[Claim](
	[UserClaimK] [int] IDENTITY(1,1) NOT NULL,
	[UserFK] [int] NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Identity.Claim] PRIMARY KEY CLUSTERED 
(
	[UserClaimK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Identity].[User]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Identity].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[PasswordHash] [nvarchar](512) NULL,
	[PhoneNumber] [nvarchar](128) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NULL,
	[CreatedAtUtc] [datetime] NOT NULL,
	[CreatedByUserFK] [int] NOT NULL,
	[ModifiedAtUtc] [datetime] NULL,
	[ModifiedByUserFK] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastLoginDateUtc] [datetime] NULL,
	[PasswordExpirationDateUtc] [datetime] NULL,
	[Status] [int] NULL,
	[ChangePasswordFailedCount] [int] NOT NULL,
	[LoginOption] [int] NULL,
	[DateOfBirth] [datetime] NULL,
	[PersonalEmailAddress] [nvarchar](64) NULL,
	[MobileNumber] [nvarchar](64) NULL,
	[ImageFileName] [nvarchar](256) NULL,
	[PasswordSalt] [varchar](300) NULL,
 CONSTRAINT [PK_Identity.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Identity].[UserLogin]    Script Date: 7/18/2021 6:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Identity].[UserLogin](
	[UserLoginK] [int] IDENTITY(1,1) NOT NULL,
	[UserFK] [int] NOT NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
 CONSTRAINT [PK_Identity.UserLogin] PRIMARY KEY CLUSTERED 
(
	[UserLoginK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AccessTokens] ON 

INSERT [dbo].[AccessTokens] ([AccessTokenId], [Token], [UserName], [EffectiveTime], [ExpiresIn], [UserAgent], [IP]) VALUES (18, N'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIkFkbWluIiwiQWRtaW4yIl0sInVybjptYWlhbnZhdDpVc2VySWQiOiIxIiwiZXhwIjoxNjI2OTQwNTgwLCJpc3MiOiJ1cm46bWFpYW52YXQ6aXNzdWVyIiwiYXVkIjoidXJuOm1haWFudmF0OmF1ZGllbmNlIn0.4empKTHe-Pw-ytYLza3wHGsWOhtZErvFVuCobq44G6Q                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ', N'system@', CAST(N'2021-07-18T10:56:20.9083657+07:00' AS DateTimeOffset), 360000, N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36', N'')
INSERT [dbo].[AccessTokens] ([AccessTokenId], [Token], [UserName], [EffectiveTime], [ExpiresIn], [UserAgent], [IP]) VALUES (1002, N'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIkFkbWluIl0sInVybjptYWlhbnZhdDpVc2VySWQiOiIxIiwiZXhwIjoxNjI2OTQ5MjA3LCJpc3MiOiJ1cm46bWFpYW52YXQ6aXNzdWVyIiwiYXVkIjoidXJuOm1haWFudmF0OmF1ZGllbmNlIn0.pzbEWmKI6BST9AgeMMxEHupR15CR7NotnvOs9PX2-rY                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ', N'system@', CAST(N'2021-07-18T13:20:07.2048769+07:00' AS DateTimeOffset), 360000, N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36', N'')
INSERT [dbo].[AccessTokens] ([AccessTokenId], [Token], [UserName], [EffectiveTime], [ExpiresIn], [UserAgent], [IP]) VALUES (1003, N'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIkFkbWluIl0sInVybjptYWlhbnZhdDpVc2VySWQiOiIxIiwiZXhwIjoxNjI2OTQ5MjEzLCJpc3MiOiJ1cm46bWFpYW52YXQ6aXNzdWVyIiwiYXVkIjoidXJuOm1haWFudmF0OmF1ZGllbmNlIn0.OYk_vUtFf2a7KUWfwK-idlBGyhFuHA_bWlpkeQqtCbI                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ', N'system@', CAST(N'2021-07-18T13:20:13.4697467+07:00' AS DateTimeOffset), 360000, N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36', N'')
SET IDENTITY_INSERT [dbo].[AccessTokens] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryK], [Identity], [Name], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [Editable], [Description], [IsDisabled]) VALUES (N'4ccb4cb2-bae7-eb11-9606-b42e995d1b29', 6, N'Loại công việc', CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, 0, NULL, N'Danh sách loại công việc ', NULL)
INSERT [dbo].[Category] ([CategoryK], [Identity], [Name], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [Editable], [Description], [IsDisabled]) VALUES (N'4dcb4cb2-bae7-eb11-9606-b42e995d1b29', 7, N'Loại hành động', CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, 0, NULL, N'Loại hành động', NULL)
INSERT [dbo].[Category] ([CategoryK], [Identity], [Name], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [Editable], [Description], [IsDisabled]) VALUES (N'4ecb4cb2-bae7-eb11-9606-b42e995d1b29', 8, N'Loại trạng thái', CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, 0, NULL, N'Loại trạng thái', NULL)
INSERT [dbo].[Category] ([CategoryK], [Identity], [Name], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [Editable], [Description], [IsDisabled]) VALUES (N'4fcb4cb2-bae7-eb11-9606-b42e995d1b29', 9, N'Loại lịch trình', CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, 0, NULL, N'Loại lịch trình', NULL)
INSERT [dbo].[Category] ([CategoryK], [Identity], [Name], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [Editable], [Description], [IsDisabled]) VALUES (N'50cb4cb2-bae7-eb11-9606-b42e995d1b29', 10, N'Trạng thái công việc', CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, CAST(N'2021-07-18T18:24:21.623' AS DateTime), 1, 0, NULL, N'Trạng thái công việc', NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[JobType] ON 

INSERT [dbo].[JobType] ([JobTypeK], [Identity], [Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [DefaultTimeInHours], [ColorCode], [IsException], [DefaultWorkFlowFK]) VALUES (N'272169ae-0f52-4544-a6eb-4607de1d3796', 2, N'dasd', N'dasd', CAST(N'2021-07-18T07:02:58.310' AS DateTime), 1, CAST(N'2021-07-18T07:51:06.690' AS DateTime), 1, 1, 8, N'#444', 0, NULL)
INSERT [dbo].[JobType] ([JobTypeK], [Identity], [Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [DefaultTimeInHours], [ColorCode], [IsException], [DefaultWorkFlowFK]) VALUES (N'3e365d88-9a93-4085-a105-b29f7483a8ca', 3, N'dasd', N'dasd', CAST(N'2021-07-18T07:04:06.113' AS DateTime), 1, CAST(N'2021-07-18T07:04:06.113' AS DateTime), 1, 0, 8, N'#444', 0, NULL)
INSERT [dbo].[JobType] ([JobTypeK], [Identity], [Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [DefaultTimeInHours], [ColorCode], [IsException], [DefaultWorkFlowFK]) VALUES (N'e9fa86a1-10b1-4a15-8ad4-88ce2af2d518', 4, N'dasd', N'dasd', CAST(N'2021-07-18T07:06:18.083' AS DateTime), 1, CAST(N'2021-07-18T07:06:18.083' AS DateTime), 1, 0, 8, N'#444', 0, NULL)
INSERT [dbo].[JobType] ([JobTypeK], [Identity], [Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [DefaultTimeInHours], [ColorCode], [IsException], [DefaultWorkFlowFK]) VALUES (N'a0ed5d4c-6044-47c3-9163-09fcc54e1e2a', 5, N'dasd', N'dasd', CAST(N'2021-07-18T07:16:52.190' AS DateTime), 1, CAST(N'2021-07-18T07:16:52.190' AS DateTime), 1, 0, 8, N'#444', 0, NULL)
INSERT [dbo].[JobType] ([JobTypeK], [Identity], [Name], [Description], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [DefaultTimeInHours], [ColorCode], [IsException], [DefaultWorkFlowFK]) VALUES (N'dbf0dd62-afd7-4c49-b691-8d07edfb1a83', 6, N'Phi', N'Phi', CAST(N'2021-07-18T07:48:26.967' AS DateTime), 1, CAST(N'2021-07-18T07:50:11.413' AS DateTime), 1, 0, 8, N'#444', 0, NULL)
SET IDENTITY_INSERT [dbo].[JobType] OFF
GO
SET IDENTITY_INSERT [dbo].[SchemaVersions] ON 

INSERT [dbo].[SchemaVersions] ([Id], [ScriptName], [Applied]) VALUES (2005, N'MaiAnVat.Database.Up.Scripts.Script00001.Ref.ID.User.sql', CAST(N'2021-07-18T18:24:21.617' AS DateTime))
INSERT [dbo].[SchemaVersions] ([Id], [ScriptName], [Applied]) VALUES (2006, N'MaiAnVat.Database.Up.Scripts.Script00002.Category.sql', CAST(N'2021-07-18T18:24:21.623' AS DateTime))
SET IDENTITY_INSERT [dbo].[SchemaVersions] OFF
GO
SET IDENTITY_INSERT [Identity].[User] ON 

INSERT [Identity].[User] ([Id], [AccessFailedCount], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEndDateUtc], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [UserName], [FirstName], [LastName], [CreatedAtUtc], [CreatedByUserFK], [ModifiedAtUtc], [ModifiedByUserFK], [IsDeleted], [LastLoginDateUtc], [PasswordExpirationDateUtc], [Status], [ChangePasswordFailedCount], [LoginOption], [DateOfBirth], [PersonalEmailAddress], [MobileNumber], [ImageFileName], [PasswordSalt]) VALUES (1, 0, N'system@', 1, 0, NULL, N'86e69fc9fa9314555c4cfcbb4c8addcf', NULL, 0, N'system@', N'SYSTEM', N'', CAST(N'2016-06-15T07:45:38.833' AS DateTime), 1, CAST(N'2016-06-15T07:45:38.837' AS DateTime), 1, 0, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, N'6hf71')
SET IDENTITY_INSERT [Identity].[User] OFF
GO
/****** Object:  Index [PK__Category__0801D9A56A6FA8F4]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [PK__Category__0801D9A56A6FA8F4] PRIMARY KEY NONCLUSTERED 
(
	[CategoryK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Category__F271C41026BB9FE3]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[CategoryClassification] ADD PRIMARY KEY NONCLUSTERED 
(
	[CategoryClassificationK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Group__D10DF35876D916B6]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[Group] ADD PRIMARY KEY NONCLUSTERED 
(
	[GroupK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__GroupPer__77B40ED12C09340A]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[GroupPermission] ADD PRIMARY KEY NONCLUSTERED 
(
	[GroupPermissionK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Job__9E749C2A6299A70A]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[Job] ADD PRIMARY KEY NONCLUSTERED 
(
	[JobK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__JobAttac__789D83C78E397D5E]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[JobAttachment] ADD PRIMARY KEY NONCLUSTERED 
(
	[JobAttachmentK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__JobMessa__0FB9948A11FE0270]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[JobMessage] ADD PRIMARY KEY NONCLUSTERED 
(
	[MessageK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__JobType__1DA7FD3DB2B525E6]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[JobType] ADD PRIMARY KEY NONCLUSTERED 
(
	[JobTypeK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__ListCate__9434FEB670354244]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[ListCategory] ADD  CONSTRAINT [PK__ListCate__9434FEB670354244] PRIMARY KEY NONCLUSTERED 
(
	[ListCategoryK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__MavMenu__70F3A95FCBE90F53]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[MavMenu] ADD PRIMARY KEY NONCLUSTERED 
(
	[MavMenuK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Notifica__A72F067DBCA36FEA]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[Notification] ADD PRIMARY KEY NONCLUSTERED 
(
	[NotificationK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Permissi__C5E77B0EF108572D]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[Permission] ADD PRIMARY KEY NONCLUSTERED 
(
	[PermissionK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Permissi__619CC5209C7CF545]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[PermissionType] ADD PRIMARY KEY NONCLUSTERED 
(
	[PermissionTypeK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__RefreshT__FE60091BC480F178]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[RefreshToken] ADD PRIMARY KEY NONCLUSTERED 
(
	[RefreshTokenK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Registra__BB73567E75D3B560]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[RegistrationJob] ADD PRIMARY KEY NONCLUSTERED 
(
	[RegistrationJobK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Schedule__FA9A8324BA664F6C]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[Schedule] ADD PRIMARY KEY NONCLUSTERED 
(
	[ScheduleK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__Schedule__B65DE38F260B8068]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[ScheduleHistory] ADD PRIMARY KEY NONCLUSTERED 
(
	[ScheduleHistoryK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__UserGrou__240A7FB77552F06B]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[UserGroup] ADD PRIMARY KEY NONCLUSTERED 
(
	[UserGroupK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__UserLogi__BB63EF7250AD5D13]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[UserLoginHistory] ADD PRIMARY KEY NONCLUSTERED 
(
	[UserLoginHistoryK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__UserNoti__7791942798C4EB07]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[UserNotification] ADD PRIMARY KEY NONCLUSTERED 
(
	[UserNotificationK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK__WorkFlow__5FC254F176CAF718]    Script Date: 7/18/2021 6:54:32 PM ******/
ALTER TABLE [dbo].[WorkFlowStatus] ADD PRIMARY KEY NONCLUSTERED 
(
	[WorkFlowStatusK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF__Category__Catego__398D8EEE]  DEFAULT (newsequentialid()) FOR [CategoryK]
GO
ALTER TABLE [dbo].[CategoryClassification] ADD  DEFAULT (newsequentialid()) FOR [CategoryClassificationK]
GO
ALTER TABLE [dbo].[FileStorage] ADD  DEFAULT (newsequentialid()) FOR [FileStorageK]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (newsequentialid()) FOR [GroupK]
GO
ALTER TABLE [dbo].[GroupPermission] ADD  DEFAULT (newsequentialid()) FOR [GroupPermissionK]
GO
ALTER TABLE [dbo].[Job] ADD  DEFAULT (newsequentialid()) FOR [JobK]
GO
ALTER TABLE [dbo].[JobAssignedUser] ADD  CONSTRAINT [DF_JobAssignedUser_JobAssignedUserK]  DEFAULT (newsequentialid()) FOR [JobAssignedUserK]
GO
ALTER TABLE [dbo].[JobAssignmentList] ADD  CONSTRAINT [DF_JobAssignmentList_JobAssignmentListK]  DEFAULT (newsequentialid()) FOR [JobAssignmentListK]
GO
ALTER TABLE [dbo].[JobAssignmentListStatus] ADD  DEFAULT (newsequentialid()) FOR [JobAssignmentListStatusK]
GO
ALTER TABLE [dbo].[JobAssignmentListStatus] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[JobAttachment] ADD  DEFAULT (newsequentialid()) FOR [JobAttachmentK]
GO
ALTER TABLE [dbo].[JobMessage] ADD  DEFAULT (newsequentialid()) FOR [MessageK]
GO
ALTER TABLE [dbo].[JobType] ADD  DEFAULT (newsequentialid()) FOR [JobTypeK]
GO
ALTER TABLE [dbo].[JobType] ADD  CONSTRAINT [DF_JobType_ColorCode]  DEFAULT (N'#5bc0de') FOR [ColorCode]
GO
ALTER TABLE [dbo].[JobType] ADD  DEFAULT ((1)) FOR [IsException]
GO
ALTER TABLE [dbo].[JobTypeWorkFlow] ADD  CONSTRAINT [DF_JobTypeWorkflow_JobTypeWorkFlowK]  DEFAULT (newsequentialid()) FOR [JobTypeWorkFlowK]
GO
ALTER TABLE [dbo].[JobWorkFlowMove] ADD  CONSTRAINT [DF_JobWorkFlowMove_JobWorkFlowMoveK]  DEFAULT (newsequentialid()) FOR [JobWorkFlowMoveK]
GO
ALTER TABLE [dbo].[JobWorkFlowStatus] ADD  CONSTRAINT [DF_JobWorkFlowStatus_JobWorkFlowStatusK]  DEFAULT (newsequentialid()) FOR [JobWorkFlowStatusK]
GO
ALTER TABLE [dbo].[ListCategory] ADD  CONSTRAINT [DF__ListCateg__ListC__4BAC3F29]  DEFAULT (newsequentialid()) FOR [ListCategoryK]
GO
ALTER TABLE [dbo].[MavMenu] ADD  DEFAULT (newsequentialid()) FOR [MavMenuK]
GO
ALTER TABLE [dbo].[MavMenu] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT (newsequentialid()) FOR [NotificationK]
GO
ALTER TABLE [dbo].[Permission] ADD  DEFAULT (newsequentialid()) FOR [PermissionK]
GO
ALTER TABLE [dbo].[PermissionType] ADD  DEFAULT (newsequentialid()) FOR [PermissionTypeK]
GO
ALTER TABLE [dbo].[RefreshToken] ADD  DEFAULT (newsequentialid()) FOR [RefreshTokenK]
GO
ALTER TABLE [dbo].[RegistrationJob] ADD  DEFAULT (newsequentialid()) FOR [RegistrationJobK]
GO
ALTER TABLE [dbo].[Schedule] ADD  DEFAULT (newsequentialid()) FOR [ScheduleK]
GO
ALTER TABLE [dbo].[ScheduleHistory] ADD  DEFAULT (newsequentialid()) FOR [ScheduleHistoryK]
GO
ALTER TABLE [dbo].[UserGroup] ADD  DEFAULT (newsequentialid()) FOR [UserGroupK]
GO
ALTER TABLE [dbo].[UserLoginHistory] ADD  DEFAULT (newsequentialid()) FOR [UserLoginHistoryK]
GO
ALTER TABLE [dbo].[UserNotification] ADD  DEFAULT (newsequentialid()) FOR [UserNotificationK]
GO
ALTER TABLE [dbo].[UserNotification] ADD  DEFAULT ((0)) FOR [State]
GO
ALTER TABLE [dbo].[WorkFlowStatus] ADD  DEFAULT (newsequentialid()) FOR [WorkFlowStatusK]
GO
ALTER TABLE [Identity].[User] ADD  DEFAULT ((0)) FOR [AccessFailedCount]
GO
ALTER TABLE [Identity].[User] ADD  DEFAULT ((1)) FOR [EmailConfirmed]
GO
ALTER TABLE [Identity].[User] ADD  DEFAULT ((0)) FOR [LockoutEnabled]
GO
ALTER TABLE [Identity].[User] ADD  DEFAULT ((0)) FOR [PhoneNumberConfirmed]
GO
ALTER TABLE [Identity].[User] ADD  DEFAULT (getutcdate()) FOR [CreatedAtUtc]
GO
ALTER TABLE [Identity].[User] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [Identity].[User] ADD  DEFAULT ((0)) FOR [ChangePasswordFailedCount]
GO
ALTER TABLE [dbo].[CategoryClassification]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CategoryClassification_dbo.Category_CategoryK] FOREIGN KEY([CategoryFK])
REFERENCES [dbo].[Category] ([CategoryK])
GO
ALTER TABLE [dbo].[CategoryClassification] CHECK CONSTRAINT [FK_dbo.CategoryClassification_dbo.Category_CategoryK]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_ParentGroup] FOREIGN KEY([ParentGroupFK])
REFERENCES [dbo].[Group] ([GroupK])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_ParentGroup]
GO
ALTER TABLE [dbo].[GroupPermission]  WITH CHECK ADD  CONSTRAINT [FK_GroupPermission_ToGroup] FOREIGN KEY([GroupFK])
REFERENCES [dbo].[Group] ([GroupK])
GO
ALTER TABLE [dbo].[GroupPermission] CHECK CONSTRAINT [FK_GroupPermission_ToGroup]
GO
ALTER TABLE [dbo].[GroupPermission]  WITH CHECK ADD  CONSTRAINT [FK_GroupPermission_ToPermission] FOREIGN KEY([PermissionFK])
REFERENCES [dbo].[Permission] ([PermissionK])
GO
ALTER TABLE [dbo].[GroupPermission] CHECK CONSTRAINT [FK_GroupPermission_ToPermission]
GO
ALTER TABLE [dbo].[Job]  WITH NOCHECK ADD  CONSTRAINT [jobs.Job_JobStatusFK_dbo.ListCategory_ListCategoryK] FOREIGN KEY([JobStatusFK])
REFERENCES [dbo].[ListCategory] ([ListCategoryK])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [jobs.Job_JobStatusFK_dbo.ListCategory_ListCategoryK]
GO
ALTER TABLE [dbo].[Job]  WITH NOCHECK ADD  CONSTRAINT [jobs.Job_JobTypeFK_jobs.JobType_JobTypeK] FOREIGN KEY([JobTypeFK])
REFERENCES [dbo].[JobType] ([JobTypeK])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [jobs.Job_JobTypeFK_jobs.JobType_JobTypeK]
GO
ALTER TABLE [dbo].[Job]  WITH NOCHECK ADD  CONSTRAINT [jobs.Job_WorkflowStatusFK_dbo.WorkFlowStatus_WorkFlowStatusK] FOREIGN KEY([WorkflowStatusFK])
REFERENCES [dbo].[WorkFlowStatus] ([WorkFlowStatusK])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [jobs.Job_WorkflowStatusFK_dbo.WorkFlowStatus_WorkFlowStatusK]
GO
ALTER TABLE [dbo].[JobAssignedUser]  WITH CHECK ADD  CONSTRAINT [FK_JobAssignedUser_Job] FOREIGN KEY([JobFK])
REFERENCES [dbo].[Job] ([JobK])
GO
ALTER TABLE [dbo].[JobAssignedUser] CHECK CONSTRAINT [FK_JobAssignedUser_Job]
GO
ALTER TABLE [dbo].[JobAssignedUser]  WITH CHECK ADD  CONSTRAINT [FK_JobAssignedUser_JobAssignmentList] FOREIGN KEY([JobAssignmentListFK])
REFERENCES [dbo].[JobAssignmentList] ([JobAssignmentListK])
GO
ALTER TABLE [dbo].[JobAssignedUser] CHECK CONSTRAINT [FK_JobAssignedUser_JobAssignmentList]
GO
ALTER TABLE [dbo].[JobAssignedUser]  WITH CHECK ADD  CONSTRAINT [FK_JobAssignedUser_User] FOREIGN KEY([UserFK])
REFERENCES [Identity].[User] ([Id])
GO
ALTER TABLE [dbo].[JobAssignedUser] CHECK CONSTRAINT [FK_JobAssignedUser_User]
GO
ALTER TABLE [dbo].[JobAssignmentList]  WITH CHECK ADD  CONSTRAINT [FK_JobAssignmentList_Group] FOREIGN KEY([GroupFK])
REFERENCES [dbo].[Group] ([GroupK])
GO
ALTER TABLE [dbo].[JobAssignmentList] CHECK CONSTRAINT [FK_JobAssignmentList_Group]
GO
ALTER TABLE [dbo].[JobAssignmentList]  WITH CHECK ADD  CONSTRAINT [FK_JobAssignmentList_JobType] FOREIGN KEY([JobTypeFK])
REFERENCES [dbo].[JobType] ([JobTypeK])
GO
ALTER TABLE [dbo].[JobAssignmentList] CHECK CONSTRAINT [FK_JobAssignmentList_JobType]
GO
ALTER TABLE [dbo].[JobAssignmentListStatus]  WITH CHECK ADD  CONSTRAINT [FK_JobAssignmentListStatus_ToJobAssignmentList] FOREIGN KEY([JobAssignmentListFK])
REFERENCES [dbo].[JobAssignmentList] ([JobAssignmentListK])
GO
ALTER TABLE [dbo].[JobAssignmentListStatus] CHECK CONSTRAINT [FK_JobAssignmentListStatus_ToJobAssignmentList]
GO
ALTER TABLE [dbo].[JobAssignmentListStatus]  WITH CHECK ADD  CONSTRAINT [FK_JobAssignmentListStatus_ToWorkFlowStatus] FOREIGN KEY([WorkFlowStatusFK])
REFERENCES [dbo].[WorkFlowStatus] ([WorkFlowStatusK])
GO
ALTER TABLE [dbo].[JobAssignmentListStatus] CHECK CONSTRAINT [FK_JobAssignmentListStatus_ToWorkFlowStatus]
GO
ALTER TABLE [dbo].[JobAttachment]  WITH CHECK ADD  CONSTRAINT [FK.JobAttachment_FileStorageFK.FileStorage_FileStorageK] FOREIGN KEY([FileStorageFK])
REFERENCES [dbo].[FileStorage] ([FileStorageK])
GO
ALTER TABLE [dbo].[JobAttachment] CHECK CONSTRAINT [FK.JobAttachment_FileStorageFK.FileStorage_FileStorageK]
GO
ALTER TABLE [dbo].[JobAttachment]  WITH CHECK ADD  CONSTRAINT [FK.JobAttachment_ListCategoryFK.ListCategory_ListCategoryK] FOREIGN KEY([ListCategoryFK])
REFERENCES [dbo].[ListCategory] ([ListCategoryK])
GO
ALTER TABLE [dbo].[JobAttachment] CHECK CONSTRAINT [FK.JobAttachment_ListCategoryFK.ListCategory_ListCategoryK]
GO
ALTER TABLE [dbo].[JobMessage]  WITH CHECK ADD  CONSTRAINT [jobs.JobMessage_JobFK_jobs.Job_JobK] FOREIGN KEY([JobFK])
REFERENCES [dbo].[Job] ([JobK])
GO
ALTER TABLE [dbo].[JobMessage] CHECK CONSTRAINT [jobs.JobMessage_JobFK_jobs.Job_JobK]
GO
ALTER TABLE [dbo].[JobMessage]  WITH CHECK ADD  CONSTRAINT [jobs.JobMessage_MessageFK_jobs.JobMessage_MessageK] FOREIGN KEY([MessageFK])
REFERENCES [dbo].[JobMessage] ([MessageK])
GO
ALTER TABLE [dbo].[JobMessage] CHECK CONSTRAINT [jobs.JobMessage_MessageFK_jobs.JobMessage_MessageK]
GO
ALTER TABLE [dbo].[JobMessage]  WITH CHECK ADD  CONSTRAINT [jobs.JobMessage_UserFK_jobs.User_Id] FOREIGN KEY([UserFK])
REFERENCES [Identity].[User] ([Id])
GO
ALTER TABLE [dbo].[JobMessage] CHECK CONSTRAINT [jobs.JobMessage_UserFK_jobs.User_Id]
GO
ALTER TABLE [dbo].[JobType]  WITH CHECK ADD  CONSTRAINT [FK_JobType_WorkFlowStatus] FOREIGN KEY([DefaultWorkFlowFK])
REFERENCES [dbo].[WorkFlowStatus] ([WorkFlowStatusK])
GO
ALTER TABLE [dbo].[JobType] CHECK CONSTRAINT [FK_JobType_WorkFlowStatus]
GO
ALTER TABLE [dbo].[JobTypeWorkFlow]  WITH CHECK ADD  CONSTRAINT [JobTypeWorkFlow_JobTypeFK.JobType_JobTypeK] FOREIGN KEY([JobTypeFK])
REFERENCES [dbo].[JobType] ([JobTypeK])
GO
ALTER TABLE [dbo].[JobTypeWorkFlow] CHECK CONSTRAINT [JobTypeWorkFlow_JobTypeFK.JobType_JobTypeK]
GO
ALTER TABLE [dbo].[JobWorkFlowMove]  WITH CHECK ADD  CONSTRAINT [FK_JobWorkFlowMove_Job] FOREIGN KEY([JobFK])
REFERENCES [dbo].[Job] ([JobK])
GO
ALTER TABLE [dbo].[JobWorkFlowMove] CHECK CONSTRAINT [FK_JobWorkFlowMove_Job]
GO
ALTER TABLE [dbo].[JobWorkFlowMove]  WITH CHECK ADD  CONSTRAINT [FK_JobWorkFlowMove_WorkFlowStatus] FOREIGN KEY([FromWorkFlowStatusFK])
REFERENCES [dbo].[WorkFlowStatus] ([WorkFlowStatusK])
GO
ALTER TABLE [dbo].[JobWorkFlowMove] CHECK CONSTRAINT [FK_JobWorkFlowMove_WorkFlowStatus]
GO
ALTER TABLE [dbo].[JobWorkFlowMove]  WITH CHECK ADD  CONSTRAINT [FK_JobWorkFlowMove_WorkFlowStatus_2] FOREIGN KEY([ToWorkFlowStatusFK])
REFERENCES [dbo].[WorkFlowStatus] ([WorkFlowStatusK])
GO
ALTER TABLE [dbo].[JobWorkFlowMove] CHECK CONSTRAINT [FK_JobWorkFlowMove_WorkFlowStatus_2]
GO
ALTER TABLE [dbo].[JobWorkFlowStatus]  WITH CHECK ADD  CONSTRAINT [JobWorkFlowStatus_FromWorkFlowStatusFK.WorkFlowStatus_WorkFlowStatusK] FOREIGN KEY([FromWorkFlowStatusFK])
REFERENCES [dbo].[WorkFlowStatus] ([WorkFlowStatusK])
GO
ALTER TABLE [dbo].[JobWorkFlowStatus] CHECK CONSTRAINT [JobWorkFlowStatus_FromWorkFlowStatusFK.WorkFlowStatus_WorkFlowStatusK]
GO
ALTER TABLE [dbo].[JobWorkFlowStatus]  WITH CHECK ADD  CONSTRAINT [JobWorkFlowStatus_JobTypeWorkFlowFK.JobTypeWorkFlow_JobTypeWorkFlowK] FOREIGN KEY([JobTypeWorkFlowFK])
REFERENCES [dbo].[JobTypeWorkFlow] ([JobTypeWorkFlowK])
GO
ALTER TABLE [dbo].[JobWorkFlowStatus] CHECK CONSTRAINT [JobWorkFlowStatus_JobTypeWorkFlowFK.JobTypeWorkFlow_JobTypeWorkFlowK]
GO
ALTER TABLE [dbo].[JobWorkFlowStatus]  WITH CHECK ADD  CONSTRAINT [JobWorkFlowStatus_ToWorkFlowStatusFK.WorkFlowStatus_WorkFlowStatusK] FOREIGN KEY([ToWorkFlowStatusFK])
REFERENCES [dbo].[WorkFlowStatus] ([WorkFlowStatusK])
GO
ALTER TABLE [dbo].[JobWorkFlowStatus] CHECK CONSTRAINT [JobWorkFlowStatus_ToWorkFlowStatusFK.WorkFlowStatus_WorkFlowStatusK]
GO
ALTER TABLE [dbo].[ListCategory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ListCategory_dbo.Category_CategoryK] FOREIGN KEY([CategoryFK])
REFERENCES [dbo].[Category] ([CategoryK])
GO
ALTER TABLE [dbo].[ListCategory] CHECK CONSTRAINT [FK_dbo.ListCategory_dbo.Category_CategoryK]
GO
ALTER TABLE [dbo].[RegistrationJob]  WITH NOCHECK ADD  CONSTRAINT [registrationjobs.RegistrationJob_JobFK_dbo.Job_JobK] FOREIGN KEY([JobFK])
REFERENCES [dbo].[Job] ([JobK])
GO
ALTER TABLE [dbo].[RegistrationJob] CHECK CONSTRAINT [registrationjobs.RegistrationJob_JobFK_dbo.Job_JobK]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [Schedule_JobFK.JobK] FOREIGN KEY([JobFK])
REFERENCES [dbo].[Job] ([JobK])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [Schedule_JobFK.JobK]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [Schedule_ScheduleTypeFK_ListCategory.ListCategoryK] FOREIGN KEY([ScheduleTypeFK])
REFERENCES [dbo].[ListCategory] ([ListCategoryK])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [Schedule_ScheduleTypeFK_ListCategory.ListCategoryK]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [Schedule_UserFK_User.User_Id] FOREIGN KEY([UserFK])
REFERENCES [Identity].[User] ([Id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [Schedule_UserFK_User.User_Id]
GO
ALTER TABLE [dbo].[ScheduleHistory]  WITH CHECK ADD  CONSTRAINT [FK_ScheduleHistory_Schedule] FOREIGN KEY([ScheduleFK])
REFERENCES [dbo].[Schedule] ([ScheduleK])
GO
ALTER TABLE [dbo].[ScheduleHistory] CHECK CONSTRAINT [FK_ScheduleHistory_Schedule]
GO
ALTER TABLE [dbo].[ScheduleHistory]  WITH CHECK ADD  CONSTRAINT [ScheduleHistory_UserFK_User.User_Id] FOREIGN KEY([UserFK])
REFERENCES [Identity].[User] ([Id])
GO
ALTER TABLE [dbo].[ScheduleHistory] CHECK CONSTRAINT [ScheduleHistory_UserFK_User.User_Id]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_ToGroup] FOREIGN KEY([UserFK])
REFERENCES [Identity].[User] ([Id])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_ToGroup]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_ToUser] FOREIGN KEY([GroupFK])
REFERENCES [dbo].[Group] ([GroupK])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_ToUser]
GO
ALTER TABLE [dbo].[UserNotification]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserNotification_dbo.Identity_User_UserFK] FOREIGN KEY([UserFK])
REFERENCES [Identity].[User] ([Id])
GO
ALTER TABLE [dbo].[UserNotification] CHECK CONSTRAINT [FK_dbo.UserNotification_dbo.Identity_User_UserFK]
GO
ALTER TABLE [dbo].[UserNotification]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserNotification_dbo.Notification_NotificationFK] FOREIGN KEY([NotificationFK])
REFERENCES [dbo].[Notification] ([NotificationK])
GO
ALTER TABLE [dbo].[UserNotification] CHECK CONSTRAINT [FK_dbo.UserNotification_dbo.Notification_NotificationFK]
GO
ALTER TABLE [Identity].[Claim]  WITH CHECK ADD  CONSTRAINT [FK_Identity.Claim_Identity.User_UserFK] FOREIGN KEY([UserFK])
REFERENCES [Identity].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Identity].[Claim] CHECK CONSTRAINT [FK_Identity.Claim_Identity.User_UserFK]
GO
ALTER TABLE [Identity].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_Identity.UserLogin_Identity.User_UserFK] FOREIGN KEY([UserFK])
REFERENCES [Identity].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Identity].[UserLogin] CHECK CONSTRAINT [FK_Identity.UserLogin_Identity.User_UserFK]
GO
