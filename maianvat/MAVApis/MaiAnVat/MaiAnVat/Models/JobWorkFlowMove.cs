using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobWorkFlowMove
    {
        public Guid JobWorkFlowMoveK { get; set; }
        public int Identity { get; set; }
        public Guid JobFk { get; set; }
        public Guid? FromWorkFlowStatusFk { get; set; }
        public Guid ToWorkFlowStatusFk { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public WorkFlowStatus FromWorkFlowStatusFkNavigation { get; set; }
        public Job JobFkNavigation { get; set; }
        public WorkFlowStatus ToWorkFlowStatusFkNavigation { get; set; }
    }
}
