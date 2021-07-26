using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoGenerateTestcase.APIs.PageFields.Dto
{
    public class GetPageFieldDto : Entity<long>
    {
        public string Name { get; set; }

        public long RequestPageId { get; set; }
        public string Type { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public bool Nullable { get; set; }
        public string Note { get; set; }
    }
}
