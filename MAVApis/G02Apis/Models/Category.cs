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
    
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.CategoryClassifications = new HashSet<CategoryClassification>();
            this.ListCategories = new HashSet<ListCategory>();
        }
    
        public System.Guid CategoryK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFK { get; set; }
        public Nullable<System.DateTime> ModifiedAtUtc { get; set; }
        public Nullable<int> ModifiedByUserFK { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<bool> Editable { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDisabled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryClassification> CategoryClassifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListCategory> ListCategories { get; set; }
    }
}
