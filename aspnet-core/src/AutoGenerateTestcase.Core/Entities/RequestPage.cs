
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using static AutoGenerateTestcase.Constants.Enum;

namespace AutoGenerateTestcase.Entities
{
    public class RequestPage : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [ForeignKey(nameof(RequestId))]
        public long RequestId { get; set; }
        public Request Request { get; set; }

        public string PageName { get; set; }
        public RequestPageType PageType { get; set; }
    }
}
