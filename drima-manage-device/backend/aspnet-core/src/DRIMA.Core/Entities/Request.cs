using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static DRIMA.Constants.Enums;

namespace DRIMA.Entities
{
    public class Request: FullAuditedEntity<Guid>
    {
        [Column("Status", TypeName = "tinyint")]
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        [Column("Type", TypeName = "tinyint")]
        public RequestType Type { get; set; }
        public Guid? DeviceId { get; set; }
        [ForeignKey(nameof(DeviceId))]
        public Device Device { get; set; }
        [Column("DeviceName", TypeName = "nvarchar(100)")]
        public string DeviceName { get; set; }
        [Column("Description", TypeName = "nvarchar(500)")]
        public string Description { get; set; }
        [Column("Priority", TypeName = "tinyint")]
        public RequestPriority Priority { get; set; }
        [Column("Reason", TypeName = "nvarchar(500)")]
        public string Reason { get; set; }
    }
}
