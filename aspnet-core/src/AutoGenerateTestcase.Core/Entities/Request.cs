using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using AutoGenerateTestcase.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static AutoGenerateTestcase.Constants.Enum;

namespace AutoGenerateTestcase.Entities
{
    public class Request : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public RequestStatus Status { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
    }
}
