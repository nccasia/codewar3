using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobTypeWorkFlow
    {
        public JobTypeWorkFlow()
        {
            JobWorkFlowStatus = new HashSet<JobWorkFlowStatus>();
        }

        public Guid JobTypeWorkFlowK { get; set; }
        public int Identity { get; set; }
        public Guid JobTypeFk { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnabled { get; set; }

        public JobType JobTypeFkNavigation { get; set; }
        public ICollection<JobWorkFlowStatus> JobWorkFlowStatus { get; set; }
    }
}
