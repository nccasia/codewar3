//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace G02Apis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobType()
        {
            this.Jobs = new HashSet<Job>();
            this.JobAssignmentLists = new HashSet<JobAssignmentList>();
            this.JobTypeWorkFlows = new HashSet<JobTypeWorkFlow>();
        }
    
        public System.Guid JobTypeK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFK { get; set; }
        public Nullable<System.DateTime> ModifiedAtUtc { get; set; }
        public Nullable<int> ModifiedByUserFK { get; set; }
        public bool IsDeleted { get; set; }
        public int DefaultTimeInHours { get; set; }
        public string ColorCode { get; set; }
        public Nullable<bool> IsException { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Jobs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobAssignmentList> JobAssignmentLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobTypeWorkFlow> JobTypeWorkFlows { get; set; }
    }
}
