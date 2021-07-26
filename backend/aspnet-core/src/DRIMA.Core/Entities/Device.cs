using Abp.Domain.Entities.Auditing;
using DRIMA.Annotations;
using DRIMA.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static DRIMA.Constants.Enums;

namespace DRIMA.Entities
{
    public class Device: FullAuditedEntity<Guid>
    {
        [Column(TypeName = "nvarchar(100)")]
        [ApplySearch]
        public string Name { get; set; }

        public DateTime? GuaranteeDate { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [ApplySearch]
        public string Manufacturer { get; set; }

        [Column(TypeName = "tinyint")]
        public DeviceStatus Status { get; set; }

        public DateTime? BorrowedDate { get; set; }

        public long? BorrowedUserId { get; set; }

        [ForeignKey(nameof(BorrowedUserId))]
        public User Borrower { get; set; }

        public string QrCode { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        [ApplySearch]
        public string Description { get; set; }

        public IList<DeviceImage> Images { get; set; } = new List<DeviceImage>();
    }
}
