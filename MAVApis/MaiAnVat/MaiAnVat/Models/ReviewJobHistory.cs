using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class ReviewJobHistory
    {
        public ReviewJobHistory()
        {
            RejectedReason = new HashSet<RejectedReason>();
        }

        public Guid ReviewJobHistoryK { get; set; }
        public int Identity { get; set; }
        public Guid ReviewStatusFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public int ReviewBy { get; set; }
        public DateTime? ReviewTimeStamp { get; set; }
        public Guid JobFk { get; set; }

        public Job JobFkNavigation { get; set; }
        public ListCategory ReviewStatusFkNavigation { get; set; }
        public ICollection<RejectedReason> RejectedReason { get; set; }
    }
}
