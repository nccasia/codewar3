using System;
using Abp.Domain.Entities.Auditing;
using static DRIMA.Constants.StatusEnum;

namespace DRIMA.Entities
{
    public class Device: FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Brand { get; set; }
        public DeviceStatus Status { get; set; }
        public DateTime BorrowDate { get; set; }
        public string Borrower { get; set; }
        public string QrCode { get; set; }
    }
}
