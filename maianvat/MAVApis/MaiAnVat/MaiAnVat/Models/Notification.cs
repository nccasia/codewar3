using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class Notification
    {
        public Notification()
        {
            UserNotification = new HashSet<UserNotification>();
        }

        public Guid NotificationK { get; set; }
        public int Identity { get; set; }
        public string Data { get; set; }
        public string EntityTypeName { get; set; }
        public string EntityId { get; set; }
        public string NotificationName { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<UserNotification> UserNotification { get; set; }
    }
}
