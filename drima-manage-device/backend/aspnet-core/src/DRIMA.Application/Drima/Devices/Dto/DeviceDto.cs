using Abp.Application.Services.Dto;
using System;
using static DRIMA.Constants.Enums;

namespace DRIMA.Drima.Devices.Dto
{
    public class DeviceDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DeviceStatus Status { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public long? BorrowedUserId { get; set; }
        public string Description { get; set; }
        public string QrCodeBase64 { get; set; }
        public DateTime? GuaranteeDate { get; set; }
    }
}
