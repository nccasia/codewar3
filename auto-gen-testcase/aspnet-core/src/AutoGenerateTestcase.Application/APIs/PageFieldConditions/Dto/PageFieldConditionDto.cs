using Abp.AutoMapper;
using Abp.Domain.Entities;
using AutoGenerateTestcase.Entities;

using static AutoGenerateTestcase.Constants.Enum;

namespace AutoGenerateTestcase.APIs.PageFieldConditions.Dto
{
    [AutoMapTo(typeof(PageFieldCondition))]
    public class PageFieldConditionDto : Entity<long>
    {
        public PageFieldConditionType Type { get; set; }
        public string Description { get; set; }

        public long PageFieldId { get; set; }

        public long? DependPageFieldId { get; set; }
    }
}
