using Abp.Application.Services.Dto;
using System;
using static DRIMA.Constants.Enums;

namespace DRIMA.Drima.Requests.Dto
{
    public class RequestDto : CreationAuditedEntityDto<Guid>
    {
        public string Status { get; set; }
        public string Type { get; set; }
        public Guid? DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Reason { get; set; }
    }
}
