using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class MavMenu
    {
        public Guid MavMenuK { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuType { get; set; }
        public string UiSref { get; set; }
        public string UiSrefActiveIf { get; set; }
        public int? ParentMenuId { get; set; }
        public byte? MenuOrder { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public int? MenuLevel { get; set; }
        public bool IsDeleted { get; set; }
        public string DisplayMenuName { get; set; }
        public string FeatureKey { get; set; }
    }
}
