using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            ScheduleHistory = new HashSet<ScheduleHistory>();
        }

        public Guid ScheduleK { get; set; }
        public int Identity { get; set; }
        public Guid JobFk { get; set; }
        public Guid ScheduleTypeFk { get; set; }
        public int UserFk { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int Lock { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public Job JobFkNavigation { get; set; }
        public ListCategory ScheduleTypeFkNavigation { get; set; }
        public User UserFkNavigation { get; set; }
        public ICollection<ScheduleHistory> ScheduleHistory { get; set; }
    }
}
