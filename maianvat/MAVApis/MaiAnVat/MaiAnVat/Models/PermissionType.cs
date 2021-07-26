using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class PermissionType
    {
        public Guid PermissionTypeK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public int? OrderOfPrecedence { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
    }
}
