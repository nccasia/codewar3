using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class ScheduleHistory
    {
        public Guid ScheduleHistoryK { get; set; }
        public int Identity { get; set; }
        public Guid ScheduleFk { get; set; }
        public string Action { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public Schedule ScheduleFkNavigation { get; set; }
        public User UserFkNavigation { get; set; }
    }
}
