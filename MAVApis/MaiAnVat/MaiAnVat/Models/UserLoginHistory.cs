using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class UserLoginHistory
    {
        public Guid UserLoginHistoryK { get; set; }
        public int Identity { get; set; }
        public string UserOperation { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
    }
}
