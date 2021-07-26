using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using static AutoGenerateTestcase.Constants.Enum;

namespace AutoGenerateTestcase.Entities
{
    public class PageFieldCondition : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public PageFieldConditionType Type { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(PageFieldId))]
        public long PageFieldId { get; set; }
        public PageField PageField { get; set; }

        [ForeignKey(nameof(DependPageFieldId))]
        public long? DependPageFieldId { get; set; }
        public PageField DependPageField { get; set; }
    }
}
