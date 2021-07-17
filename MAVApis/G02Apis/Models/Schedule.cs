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
    
    public partial class Schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Schedule()
        {
            this.ScheduleHistories = new HashSet<ScheduleHistory>();
        }
    
        public System.Guid ScheduleK { get; set; }
        public int Identity { get; set; }
        public System.Guid JobFK { get; set; }
        public System.Guid ScheduleTypeFK { get; set; }
        public int UserFK { get; set; }
        public System.DateTime PlannedStartDate { get; set; }
        public System.DateTime PlannedEndDate { get; set; }
        public Nullable<System.DateTime> ActualStartDate { get; set; }
        public Nullable<System.DateTime> ActualEndDate { get; set; }
        public int Lock { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFK { get; set; }
        public Nullable<System.DateTime> ModifiedAtUtc { get; set; }
        public Nullable<int> ModifiedByUserFK { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Job Job { get; set; }
        public virtual ListCategory ListCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleHistory> ScheduleHistories { get; set; }
        public virtual User User { get; set; }
    }
}
