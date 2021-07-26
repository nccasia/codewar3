using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobAssignedUser
    {
        public Guid JobAssignedUserK { get; set; }
        public int Identity { get; set; }
        public Guid JobFk { get; set; }
        public int UserFk { get; set; }
        public Guid JobAssignmentListFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public JobAssignmentList JobAssignmentListFkNavigation { get; set; }
        public Job JobFkNavigation { get; set; }
        public User UserFkNavigation { get; set; }
    }
}
