using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class ListCategory
    {
        public ListCategory()
        {
            Job = new HashSet<Job>();
            JobAttachment = new HashSet<JobAttachment>();
            Schedule = new HashSet<Schedule>();
        }

        public Guid ListCategoryK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public Guid? CategoryFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public bool? IsDisabled { get; set; }

        public Category CategoryFkNavigation { get; set; }
        public ICollection<Job> Job { get; set; }
        public ICollection<JobAttachment> JobAttachment { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
    }
}
