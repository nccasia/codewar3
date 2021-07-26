using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class GroupPermission
    {
        public Guid GroupPermissionK { get; set; }
        public int Identity { get; set; }
        public Guid? GroupFk { get; set; }
        public Guid? PermissionFk { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public Group GroupFkNavigation { get; set; }
        public Permission PermissionFkNavigation { get; set; }
    }
}
