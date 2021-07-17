using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobType
    {
        public JobType()
        {
            Job = new HashSet<Job>();
            JobAssignmentList = new HashSet<JobAssignmentList>();
            JobTypeWorkFlow = new HashSet<JobTypeWorkFlow>();
        }

        public Guid JobTypeK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public int DefaultTimeInHours { get; set; }
        public string ColorCode { get; set; }
        public bool? IsException { get; set; }

        public ICollection<Job> Job { get; set; }
        public ICollection<JobAssignmentList> JobAssignmentList { get; set; }
        public ICollection<JobTypeWorkFlow> JobTypeWorkFlow { get; set; }
    }
}
