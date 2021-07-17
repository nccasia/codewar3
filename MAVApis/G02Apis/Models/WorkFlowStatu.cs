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
    
    public partial class WorkFlowStatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkFlowStatu()
        {
            this.Jobs = new HashSet<Job>();
            this.JobAssignmentListStatus = new HashSet<JobAssignmentListStatu>();
            this.JobWorkFlowMoves = new HashSet<JobWorkFlowMove>();
            this.JobWorkFlowMoves1 = new HashSet<JobWorkFlowMove>();
            this.JobWorkFlowStatus = new HashSet<JobWorkFlowStatu>();
            this.JobWorkFlowStatus1 = new HashSet<JobWorkFlowStatu>();
        }
    
        public System.Guid WorkFlowStatusK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> PreviousStatusK { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFK { get; set; }
        public Nullable<System.DateTime> ModifiedAtUtc { get; set; }
        public Nullable<int> ModifiedByUserFK { get; set; }
        public bool IsDeleted { get; set; }
        public string StatusCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Jobs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobAssignmentListStatu> JobAssignmentListStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobWorkFlowMove> JobWorkFlowMoves { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobWorkFlowMove> JobWorkFlowMoves1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobWorkFlowStatu> JobWorkFlowStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobWorkFlowStatu> JobWorkFlowStatus1 { get; set; }
    }
}
