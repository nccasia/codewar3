using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class WorkFlowStatus
    {
        public WorkFlowStatus()
        {
            Job = new HashSet<Job>();
            JobAssignmentListStatus = new HashSet<JobAssignmentListStatus>();
            JobType = new HashSet<JobType>();
            JobWorkFlowMoveFromWorkFlowStatusFkNavigation = new HashSet<JobWorkFlowMove>();
            JobWorkFlowMoveToWorkFlowStatusFkNavigation = new HashSet<JobWorkFlowMove>();
            JobWorkFlowStatusFromWorkFlowStatusFkNavigation = new HashSet<JobWorkFlowStatus>();
            JobWorkFlowStatusToWorkFlowStatusFkNavigation = new HashSet<JobWorkFlowStatus>();
        }

        public Guid WorkFlowStatusK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? PreviousStatusK { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public string StatusCode { get; set; }

        public ICollection<Job> Job { get; set; }
        public ICollection<JobAssignmentListStatus> JobAssignmentListStatus { get; set; }
        public ICollection<JobType> JobType { get; set; }
        public ICollection<JobWorkFlowMove> JobWorkFlowMoveFromWorkFlowStatusFkNavigation { get; set; }
        public ICollection<JobWorkFlowMove> JobWorkFlowMoveToWorkFlowStatusFkNavigation { get; set; }
        public ICollection<JobWorkFlowStatus> JobWorkFlowStatusFromWorkFlowStatusFkNavigation { get; set; }
        public ICollection<JobWorkFlowStatus> JobWorkFlowStatusToWorkFlowStatusFkNavigation { get; set; }
    }
}
