using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class RejectedReason
    {
        public Guid RejectedReasonK { get; set; }
        public int Identity { get; set; }
        public Guid ReasonFk { get; set; }
        public Guid ReviewJobHistoryFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public ListCategory ReasonFkNavigation { get; set; }
        public ReviewJobHistory ReviewJobHistoryFkNavigation { get; set; }
    }
}
