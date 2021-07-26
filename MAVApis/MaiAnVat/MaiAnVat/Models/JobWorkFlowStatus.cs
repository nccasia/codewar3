using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobWorkFlowStatus
    {
        public Guid JobWorkFlowStatusK { get; set; }
        public int Identity { get; set; }
        public Guid JobTypeWorkFlowFk { get; set; }
        public Guid? FromWorkFlowStatusFk { get; set; }
        public Guid ToWorkFlowStatusFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public WorkFlowStatus FromWorkFlowStatusFkNavigation { get; set; }
        public JobTypeWorkFlow JobTypeWorkFlowFkNavigation { get; set; }
        public WorkFlowStatus ToWorkFlowStatusFkNavigation { get; set; }
    }
}
