using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class Job
    {
        public Job()
        {
            JobAssignedUser = new HashSet<JobAssignedUser>();
            JobMessage = new HashSet<JobMessage>();
            JobWorkFlowMove = new HashSet<JobWorkFlowMove>();
            RegistrationJob = new HashSet<RegistrationJob>();
            Schedule = new HashSet<Schedule>();
        }

        public Guid JobK { get; set; }
        public int Identity { get; set; }
        public Guid JobTypeFk { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeactivatedUtc { get; set; }
        public int? DeactivatedUserFk { get; set; }
        public Guid? WorkflowStatusFk { get; set; }
        public string CustomerOrder { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public bool? IsSubmitted { get; set; }

        public JobType JobTypeFkNavigation { get; set; }
        public WorkFlowStatus WorkflowStatusFkNavigation { get; set; }
        public ICollection<JobAssignedUser> JobAssignedUser { get; set; }
        public ICollection<JobMessage> JobMessage { get; set; }
        public ICollection<JobWorkFlowMove> JobWorkFlowMove { get; set; }
        public ICollection<RegistrationJob> RegistrationJob { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
    }
}
