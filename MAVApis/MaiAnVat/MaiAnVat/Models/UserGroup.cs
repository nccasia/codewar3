using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class UserGroup
    {
        public Guid UserGroupK { get; set; }
        public int Identity { get; set; }
        public Guid? GroupFk { get; set; }
        public int? UserFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public Group GroupFkNavigation { get; set; }
        public User UserFkNavigation { get; set; }
    }
}
