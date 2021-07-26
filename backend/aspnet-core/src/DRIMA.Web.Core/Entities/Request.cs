using Abp.Domain.Entities.Auditing;
using static DRIMA.Constants.StatusEnum;

namespace DRIMA.Entities
{
    public class Request: FullAuditedEntity<long>
    {
        public long RequestByUserId { get; set; }
        public long RequestForUserId { get; set; }
        public RequestStatus Status { get; set; }
        public RequestType Type { get; set; }
        public long DeviceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public RequestPriority Priority { get; set; }
    }
}
