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
    
    public partial class MavMenu
    {
        public System.Guid MavMenuK { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuType { get; set; }
        public string UiSref { get; set; }
        public string UiSrefActiveIf { get; set; }
        public Nullable<int> ParentMenuID { get; set; }
        public Nullable<byte> MenuOrder { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFK { get; set; }
        public Nullable<System.DateTime> ModifiedAtUtc { get; set; }
        public Nullable<int> ModifiedByUserFK { get; set; }
        public Nullable<int> MenuLevel { get; set; }
        public bool IsDeleted { get; set; }
        public string DisplayMenuName { get; set; }
        public string FeatureKey { get; set; }
    }
}
