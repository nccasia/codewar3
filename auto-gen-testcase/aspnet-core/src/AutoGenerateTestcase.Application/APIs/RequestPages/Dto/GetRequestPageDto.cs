using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoGenerateTestcase.APIs.RequestPages.Dto
{
    public class GetRequestPageDto : Entity<long>
    {
        public long RequestId { get; set; }
        public string PageName { get; set; }
        public string PageType { get; set; }
    }
}
