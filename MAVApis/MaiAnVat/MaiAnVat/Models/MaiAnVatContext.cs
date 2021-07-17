using System;
using Microsoft.EntityFrameworkCore;

namespace MaiAnVat.Models
{
    public partial class MaiAnVatContext : DbContext
    {
        public MaiAnVatContext()
        {
        }

        public MaiAnVatContext(DbContextOptions<MaiAnVatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessTokens> AccessTokens { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryClassification> CategoryClassification { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<FileStorage> FileStorage { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupPermission> GroupPermission { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobAssignedUser> JobAssignedUser { get; set; }
        public virtual DbSet<JobAssignmentList> JobAssignmentList { get; set; }
        public virtual DbSet<JobAssignmentListStatus> JobAssignmentListStatus { get; set; }
        public virtual DbSet<JobMessage> JobMessage { get; set; }
        public virtual DbSet<JobType> JobType { get; set; }
        public virtual DbSet<JobTypeWorkFlow> JobTypeWorkFlow { get; set; }
        public virtual DbSet<JobWorkFlowMove> JobWorkFlowMove { get; set; }
        public virtual DbSet<JobWorkFlowStatus> JobWorkFlowStatus { get; set; }
        public virtual DbSet<ListCategory> ListCategory { get; set; }
        public virtual DbSet<MavMenu> MavMenu { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PermissionType> PermissionType { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<RegistrationJob> RegistrationJob { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ScheduleHistory> ScheduleHistory { get; set; }
        public virtual DbSet<SchemaVersions> SchemaVersions { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public virtual DbSet<WorkFlowStatus> WorkFlowStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-3CBIQ5M\\SQLEXPRESS;Database=MaiAnVat;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessTokens>(entity =>
            {
                entity.HasKey(e => e.AccessTokenId);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserAgent)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.CategoryK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Description).HasMaxLength(2024);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryClassification>(entity =>
            {
                entity.HasKey(e => e.CategoryClassificationK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.CategoryClassificationK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CategoryFk).HasColumnName("CategoryFK");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Description).HasMaxLength(2024);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.HasOne(d => d.CategoryFkNavigation)
                    .WithMany(p => p.CategoryClassification)
                    .HasForeignKey(d => d.CategoryFk)
                    .HasConstraintName("FK_dbo.CategoryClassification_dbo.Category_CategoryK");
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(e => e.UserClaimK);

                entity.ToTable("Claim", "Identity");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.Claim)
                    .HasForeignKey(d => d.UserFk)
                    .HasConstraintName("FK_Identity.Claim_Identity.User_UserFK");
            });

            modelBuilder.Entity<FileStorage>(entity =>
            {
                entity.HasKey(e => e.FileStorageK);

                entity.Property(e => e.FileStorageK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.FileFolder)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GroupK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.GroupK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ParentGroupFk).HasColumnName("ParentGroupFK");

                entity.HasOne(d => d.ParentGroupFkNavigation)
                    .WithMany(p => p.InverseParentGroupFkNavigation)
                    .HasForeignKey(d => d.ParentGroupFk)
                    .HasConstraintName("FK_Group_ParentGroup");
            });

            modelBuilder.Entity<GroupPermission>(entity =>
            {
                entity.HasKey(e => e.GroupPermissionK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.GroupPermissionK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.GroupFk).HasColumnName("GroupFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.PermissionFk).HasColumnName("PermissionFK");

                entity.HasOne(d => d.GroupFkNavigation)
                    .WithMany(p => p.GroupPermission)
                    .HasForeignKey(d => d.GroupFk)
                    .HasConstraintName("FK_GroupPermission_ToGroup");

                entity.HasOne(d => d.PermissionFkNavigation)
                    .WithMany(p => p.GroupPermission)
                    .HasForeignKey(d => d.PermissionFk)
                    .HasConstraintName("FK_GroupPermission_ToPermission");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.JobK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.CustomerOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeactivatedUserFk).HasColumnName("DeactivatedUserFK");

                entity.Property(e => e.DeactivatedUtc).HasColumnType("datetime");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobStatusFk).HasColumnName("JobStatusFK");

                entity.Property(e => e.JobTypeFk).HasColumnName("JobTypeFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.WorkflowStatusFk).HasColumnName("WorkflowStatusFK");

                entity.HasOne(d => d.JobStatusFkNavigation)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.JobStatusFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("jobs.Job_JobStatusFK_dbo.ListCategory_ListCategoryK");

                entity.HasOne(d => d.JobTypeFkNavigation)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.JobTypeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("jobs.Job_JobTypeFK_jobs.JobType_JobTypeK");

                entity.HasOne(d => d.WorkflowStatusFkNavigation)
                    .WithMany(p => p.Job)
                    .HasForeignKey(d => d.WorkflowStatusFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("jobs.Job_WorkflowStatusFK_dbo.WorkFlowStatus_WorkFlowStatusK");
            });

            modelBuilder.Entity<JobAssignedUser>(entity =>
            {
                entity.HasKey(e => e.JobAssignedUserK);

                entity.Property(e => e.JobAssignedUserK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobAssignmentListFk).HasColumnName("JobAssignmentListFK");

                entity.Property(e => e.JobFk).HasColumnName("JobFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.JobAssignmentListFkNavigation)
                    .WithMany(p => p.JobAssignedUser)
                    .HasForeignKey(d => d.JobAssignmentListFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobAssignedUser_JobAssignmentList");

                entity.HasOne(d => d.JobFkNavigation)
                    .WithMany(p => p.JobAssignedUser)
                    .HasForeignKey(d => d.JobFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobAssignedUser_Job");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.JobAssignedUser)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobAssignedUser_User");
            });

            modelBuilder.Entity<JobAssignmentList>(entity =>
            {
                entity.HasKey(e => e.JobAssignmentListK);

                entity.Property(e => e.JobAssignmentListK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.GroupFk).HasColumnName("GroupFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobTypeFk).HasColumnName("JobTypeFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.GroupFkNavigation)
                    .WithMany(p => p.JobAssignmentList)
                    .HasForeignKey(d => d.GroupFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobAssignmentList_Group");

                entity.HasOne(d => d.JobTypeFkNavigation)
                    .WithMany(p => p.JobAssignmentList)
                    .HasForeignKey(d => d.JobTypeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobAssignmentList_JobType");
            });

            modelBuilder.Entity<JobAssignmentListStatus>(entity =>
            {
                entity.HasKey(e => e.JobAssignmentListStatusK);

                entity.Property(e => e.JobAssignmentListStatusK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobAssignmentListFk).HasColumnName("JobAssignmentListFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.WorkFlowStatusFk).HasColumnName("WorkFlowStatusFK");

                entity.HasOne(d => d.JobAssignmentListFkNavigation)
                    .WithMany(p => p.JobAssignmentListStatus)
                    .HasForeignKey(d => d.JobAssignmentListFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobAssignmentListStatus_ToJobAssignmentList");

                entity.HasOne(d => d.WorkFlowStatusFkNavigation)
                    .WithMany(p => p.JobAssignmentListStatus)
                    .HasForeignKey(d => d.WorkFlowStatusFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobAssignmentListStatus_ToWorkFlowStatus");
            });

            modelBuilder.Entity<JobMessage>(entity =>
            {
                entity.HasKey(e => e.MessageK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.MessageK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobFk).HasColumnName("JobFK");

                entity.Property(e => e.MessageFk).HasColumnName("MessageFK");

                entity.Property(e => e.MessageText)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.JobFkNavigation)
                    .WithMany(p => p.JobMessage)
                    .HasForeignKey(d => d.JobFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("jobs.JobMessage_JobFK_jobs.Job_JobK");

                entity.HasOne(d => d.MessageFkNavigation)
                    .WithMany(p => p.InverseMessageFkNavigation)
                    .HasForeignKey(d => d.MessageFk)
                    .HasConstraintName("jobs.JobMessage_MessageFK_jobs.JobMessage_MessageK");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.JobMessage)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("jobs.JobMessage_UserFK_jobs.User_Id");
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.HasKey(e => e.JobTypeK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.JobTypeK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.ColorCode)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'#5bc0de')");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.IsException).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name).HasMaxLength(128);
            });

            modelBuilder.Entity<JobTypeWorkFlow>(entity =>
            {
                entity.HasKey(e => e.JobTypeWorkFlowK);

                entity.Property(e => e.JobTypeWorkFlowK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobTypeFk).HasColumnName("JobTypeFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.JobTypeFkNavigation)
                    .WithMany(p => p.JobTypeWorkFlow)
                    .HasForeignKey(d => d.JobTypeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("JobTypeWorkFlow_JobTypeFK.JobType_JobTypeK");
            });

            modelBuilder.Entity<JobWorkFlowMove>(entity =>
            {
                entity.HasKey(e => e.JobWorkFlowMoveK);

                entity.Property(e => e.JobWorkFlowMoveK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.FromWorkFlowStatusFk).HasColumnName("FromWorkFlowStatusFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobFk).HasColumnName("JobFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.ToWorkFlowStatusFk).HasColumnName("ToWorkFlowStatusFK");

                entity.HasOne(d => d.FromWorkFlowStatusFkNavigation)
                    .WithMany(p => p.JobWorkFlowMoveFromWorkFlowStatusFkNavigation)
                    .HasForeignKey(d => d.FromWorkFlowStatusFk)
                    .HasConstraintName("FK_JobWorkFlowMove_WorkFlowStatus");

                entity.HasOne(d => d.JobFkNavigation)
                    .WithMany(p => p.JobWorkFlowMove)
                    .HasForeignKey(d => d.JobFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobWorkFlowMove_Job");

                entity.HasOne(d => d.ToWorkFlowStatusFkNavigation)
                    .WithMany(p => p.JobWorkFlowMoveToWorkFlowStatusFkNavigation)
                    .HasForeignKey(d => d.ToWorkFlowStatusFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobWorkFlowMove_WorkFlowStatus_2");
            });

            modelBuilder.Entity<JobWorkFlowStatus>(entity =>
            {
                entity.HasKey(e => e.JobWorkFlowStatusK);

                entity.Property(e => e.JobWorkFlowStatusK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.FromWorkFlowStatusFk).HasColumnName("FromWorkFlowStatusFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobTypeWorkFlowFk).HasColumnName("JobTypeWorkFlowFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.ToWorkFlowStatusFk).HasColumnName("ToWorkFlowStatusFK");

                entity.HasOne(d => d.FromWorkFlowStatusFkNavigation)
                    .WithMany(p => p.JobWorkFlowStatusFromWorkFlowStatusFkNavigation)
                    .HasForeignKey(d => d.FromWorkFlowStatusFk)
                    .HasConstraintName("JobWorkFlowStatus_FromWorkFlowStatusFK.WorkFlowStatus_WorkFlowStatusK");

                entity.HasOne(d => d.JobTypeWorkFlowFkNavigation)
                    .WithMany(p => p.JobWorkFlowStatus)
                    .HasForeignKey(d => d.JobTypeWorkFlowFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("JobWorkFlowStatus_JobTypeWorkFlowFK.JobTypeWorkFlow_JobTypeWorkFlowK");

                entity.HasOne(d => d.ToWorkFlowStatusFkNavigation)
                    .WithMany(p => p.JobWorkFlowStatusToWorkFlowStatusFkNavigation)
                    .HasForeignKey(d => d.ToWorkFlowStatusFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("JobWorkFlowStatus_ToWorkFlowStatusFK.WorkFlowStatus_WorkFlowStatusK");
            });

            modelBuilder.Entity<ListCategory>(entity =>
            {
                entity.HasKey(e => e.ListCategoryK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ListCategoryK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CategoryFk).HasColumnName("CategoryFK");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Description).HasMaxLength(2024);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.HasOne(d => d.CategoryFkNavigation)
                    .WithMany(p => p.ListCategory)
                    .HasForeignKey(d => d.CategoryFk)
                    .HasConstraintName("FK_dbo.ListCategory_dbo.Category_CategoryK");
            });

            modelBuilder.Entity<MavMenu>(entity =>
            {
                entity.HasKey(e => e.MavMenuK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.MavMenuK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.DisplayMenuName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FeatureKey).HasMaxLength(200);

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MenuType).HasMaxLength(64);

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");

                entity.Property(e => e.UiSref).HasMaxLength(255);

                entity.Property(e => e.UiSrefActiveIf).HasMaxLength(255);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.NotificationK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.EntityId).HasMaxLength(96);

                entity.Property(e => e.EntityTypeName).HasMaxLength(250);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.NotificationName)
                    .IsRequired()
                    .HasMaxLength(96);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.PermissionK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.PermissionK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.GroupingName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<PermissionType>(entity =>
            {
                entity.HasKey(e => e.PermissionTypeK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.PermissionTypeK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.RefreshTokenK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.RefreshTokenK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.ExpiresUtc).HasColumnType("datetime");

                entity.Property(e => e.IssuedUtc).HasColumnType("datetime");

                entity.Property(e => e.ProtectedTicket)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserFk).HasColumnName("UserFK");
            });

            modelBuilder.Entity<RegistrationJob>(entity =>
            {
                entity.HasKey(e => e.RegistrationJobK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.RegistrationJobK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.ConfirmedUserFk).HasColumnName("ConfirmedUserFK");

                entity.Property(e => e.ConfirmedUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobFk).HasColumnName("JobFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.HasOne(d => d.JobFkNavigation)
                    .WithMany(p => p.RegistrationJob)
                    .HasForeignKey(d => d.JobFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registrationjobs.RegistrationJob_JobFK_dbo.Job_JobK");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ScheduleK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.ActualEndDate).HasColumnType("datetime");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.JobFk).HasColumnName("JobFK");

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.PlannedEndDate).HasColumnType("datetime");

                entity.Property(e => e.PlannedStartDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduleTypeFk).HasColumnName("ScheduleTypeFK");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.JobFkNavigation)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.JobFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Schedule_JobFK.JobK");

                entity.HasOne(d => d.ScheduleTypeFkNavigation)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.ScheduleTypeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Schedule_ScheduleTypeFK_ListCategory.ListCategoryK");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Schedule_UserFK_User.User_Id");
            });

            modelBuilder.Entity<ScheduleHistory>(entity =>
            {
                entity.HasKey(e => e.ScheduleHistoryK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ScheduleHistoryK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.ScheduleFk).HasColumnName("ScheduleFK");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.ScheduleFkNavigation)
                    .WithMany(p => p.ScheduleHistory)
                    .HasForeignKey(d => d.ScheduleFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleHistory_Schedule");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.ScheduleHistory)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ScheduleHistory_UserFK_User.User_Id");
            });

            modelBuilder.Entity<SchemaVersions>(entity =>
            {
                entity.Property(e => e.Applied).HasColumnType("datetime");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Identity");

                entity.Property(e => e.CreatedAtUtc)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.EmailConfirmed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.ImageFileName).HasMaxLength(256);

                entity.Property(e => e.LastLoginDateUtc).HasColumnType("datetime");

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.MobileNumber).HasMaxLength(64);

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.PasswordExpirationDateUtc).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash).HasMaxLength(512);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalEmailAddress).HasMaxLength(64);

                entity.Property(e => e.PhoneNumber).HasMaxLength(128);

                entity.Property(e => e.UserName).IsRequired();
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => e.UserGroupK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.UserGroupK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.GroupFk).HasColumnName("GroupFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.GroupFkNavigation)
                    .WithMany(p => p.UserGroup)
                    .HasForeignKey(d => d.GroupFk)
                    .HasConstraintName("FK_UserGroup_ToUser");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.UserGroup)
                    .HasForeignKey(d => d.UserFk)
                    .HasConstraintName("FK_UserGroup_ToGroup");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => e.UserLoginK);

                entity.ToTable("UserLogin", "Identity");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.UserLogin)
                    .HasForeignKey(d => d.UserFk)
                    .HasConstraintName("FK_Identity.UserLogin_Identity.User_UserFK");
            });

            modelBuilder.Entity<UserLoginHistory>(entity =>
            {
                entity.HasKey(e => e.UserLoginHistoryK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.UserLoginHistoryK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.Message).HasMaxLength(250);

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.UserOperation).HasMaxLength(50);
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.HasKey(e => e.UserNotificationK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.UserNotificationK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.NotificationFk).HasColumnName("NotificationFK");

                entity.Property(e => e.UserFk).HasColumnName("UserFK");

                entity.HasOne(d => d.NotificationFkNavigation)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.NotificationFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.UserNotification_dbo.Notification_NotificationFK");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.UserNotification)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.UserNotification_dbo.Identity_User_UserFK");
            });

            modelBuilder.Entity<WorkFlowStatus>(entity =>
            {
                entity.HasKey(e => e.WorkFlowStatusK)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.WorkFlowStatusK).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserFk).HasColumnName("CreatedByUserFK");

                entity.Property(e => e.Description)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Identity).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByUserFk).HasColumnName("ModifiedByUserFK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });
        }
    }
}
