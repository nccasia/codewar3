using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class RegistrationJob
    {
        public Guid RegistrationJobK { get; set; }
        public int Identity { get; set; }
        public Guid JobFk { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ConfirmedUtc { get; set; }
        public int? ConfirmedUserFk { get; set; }

        public Job JobFkNavigation { get; set; }
    }
}
