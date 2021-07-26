using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class UserNotification
    {
        public Guid UserNotificationK { get; set; }
        public int Identity { get; set; }
        public int UserFk { get; set; }
        public Guid NotificationFk { get; set; }
        public int State { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }

        public Notification NotificationFkNavigation { get; set; }
        public User UserFkNavigation { get; set; }
    }
}
