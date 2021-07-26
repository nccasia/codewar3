using DRIMA.Entities;
using System;
using static DRIMA.Constants.Enums;

namespace DRIMA.Drima.Requests.Dto
{
    public class CreateRequestDto
    {
        public Guid? Id { get; set; }
        public RequestType Type { get; set; }
        public Guid? DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public RequestPriority Priority { get; set; }
        public string Reason { get; set; }
        public RequestStatus Status { get; set; }

        public Request PatchToEntity(Request request)
        {
            if (request == null)
            {
                request = new Request { Id = Guid.NewGuid() };
            }
            request.Type = Type;
            request.DeviceId = DeviceId;
            request.DeviceName = DeviceName;
            request.Description = Description;
            request.Priority = Priority;
            request.Reason = Reason;
            request.Status = Status;

            return request;
        }
    }
}
