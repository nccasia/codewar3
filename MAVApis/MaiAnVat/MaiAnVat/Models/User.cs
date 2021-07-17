using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class User
    {
        public User()
        {
            Claim = new HashSet<Claim>();
            JobAssignedUser = new HashSet<JobAssignedUser>();
            JobMessage = new HashSet<JobMessage>();
            Schedule = new HashSet<Schedule>();
            ScheduleHistory = new HashSet<ScheduleHistory>();
            UserGroup = new HashSet<UserGroup>();
            UserLogin = new HashSet<UserLogin>();
            UserNotification = new HashSet<UserNotification>();
        }

        public int Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public DateTime? PasswordExpirationDateUtc { get; set; }
        public int? Status { get; set; }
        public int ChangePasswordFailedCount { get; set; }
        public int? LoginOption { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PersonalEmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string ImageFileName { get; set; }
        public string PasswordSalt { get; set; }

        public ICollection<Claim> Claim { get; set; }
        public ICollection<JobAssignedUser> JobAssignedUser { get; set; }
        public ICollection<JobMessage> JobMessage { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<ScheduleHistory> ScheduleHistory { get; set; }
        public ICollection<UserGroup> UserGroup { get; set; }
        public ICollection<UserLogin> UserLogin { get; set; }
        public ICollection<UserNotification> UserNotification { get; set; }
    }
}
