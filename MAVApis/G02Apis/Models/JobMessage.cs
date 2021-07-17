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
    
    public partial class JobMessage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobMessage()
        {
            this.JobMessage1 = new HashSet<JobMessage>();
        }
    
        public System.Guid MessageK { get; set; }
        public int Identity { get; set; }
        public System.Guid JobFK { get; set; }
        public int UserFK { get; set; }
        public string MessageText { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFK { get; set; }
        public Nullable<System.DateTime> ModifiedAtUtc { get; set; }
        public Nullable<int> ModifiedByUserFK { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.Guid> MessageFK { get; set; }
    
        public virtual Job Job { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobMessage> JobMessage1 { get; set; }
        public virtual JobMessage JobMessage2 { get; set; }
        public virtual User User { get; set; }
    }
}
