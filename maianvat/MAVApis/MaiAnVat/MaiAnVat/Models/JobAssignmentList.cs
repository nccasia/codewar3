using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobAssignmentList
    {
        public JobAssignmentList()
        {
            JobAssignedUser = new HashSet<JobAssignedUser>();
            JobAssignmentListStatus = new HashSet<JobAssignmentListStatus>();
        }

        public Guid JobAssignmentListK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public Guid GroupFk { get; set; }
        public Guid JobTypeFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public Group GroupFkNavigation { get; set; }
        public JobType JobTypeFkNavigation { get; set; }
        public ICollection<JobAssignedUser> JobAssignedUser { get; set; }
        public ICollection<JobAssignmentListStatus> JobAssignmentListStatus { get; set; }
    }
}
