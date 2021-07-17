using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class Permission
    {
        public Permission()
        {
            GroupPermission = new HashSet<GroupPermission>();
        }

        public Guid PermissionK { get; set; }
        public int Identity { get; set; }
        public string GroupingName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<GroupPermission> GroupPermission { get; set; }
    }
}
