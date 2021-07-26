using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobAssignmentListStatus
    {
        public Guid JobAssignmentListStatusK { get; set; }
        public int Identity { get; set; }
        public Guid JobAssignmentListFk { get; set; }
        public Guid WorkFlowStatusFk { get; set; }
        public bool IsEditable { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public JobAssignmentList JobAssignmentListFkNavigation { get; set; }
        public WorkFlowStatus WorkFlowStatusFkNavigation { get; set; }
    }
}
