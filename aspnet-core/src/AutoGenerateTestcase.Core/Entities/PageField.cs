

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using static AutoGenerateTestcase.Constants.Enum;

namespace AutoGenerateTestcase.Entities
{
    public class PageField : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(RequestPageId))]
        public long RequestPageId { get; set; }
        public RequestPage RequestPage { get; set; }

        public PageFieldType Type { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public bool Nullable { get; set; }
        public string Note { get; set; }                   
    }
}
