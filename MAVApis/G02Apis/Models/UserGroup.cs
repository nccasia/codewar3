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
    
    public partial class UserGroup
    {
        public System.Guid UserGroupK { get; set; }
        public int Identity { get; set; }
        public Nullable<System.Guid> GroupFK { get; set; }
        public Nullable<int> UserFK { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFK { get; set; }
        public Nullable<System.DateTime> ModifiedAtUtc { get; set; }
        public Nullable<int> ModifiedByUserFK { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
