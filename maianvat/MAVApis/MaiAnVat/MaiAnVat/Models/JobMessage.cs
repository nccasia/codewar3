using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobMessage
    {
        public JobMessage()
        {
            InverseMessageFkNavigation = new HashSet<JobMessage>();
        }

        public Guid MessageK { get; set; }
        public int Identity { get; set; }
        public Guid JobFk { get; set; }
        public int UserFk { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? MessageFk { get; set; }

        public Job JobFkNavigation { get; set; }
        public JobMessage MessageFkNavigation { get; set; }
        public User UserFkNavigation { get; set; }
        public ICollection<JobMessage> InverseMessageFkNavigation { get; set; }
    }
}
