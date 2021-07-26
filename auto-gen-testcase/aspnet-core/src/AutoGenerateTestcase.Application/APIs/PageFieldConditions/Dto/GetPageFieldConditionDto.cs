
using Abp.Domain.Entities;

namespace AutoGenerateTestcase.APIs.PageFieldConditions.Dto
{
    public class GetPageFieldConditionDto : Entity<long>
    {
        public string Type { get; set; }
        public string Description { get; set; }

        public long PageFieldId { get; set; }
        public string PageFieldName { get; set; }

        public long? DependPageFieldId { get; set; }
        public string DependPageFieldName { get; set; }
    }
}
