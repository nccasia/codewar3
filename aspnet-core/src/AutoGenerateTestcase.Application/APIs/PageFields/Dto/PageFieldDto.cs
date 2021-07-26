using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AutoGenerateTestcase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static AutoGenerateTestcase.Constants.Enum;

namespace AutoGenerateTestcase.APIs.PageFields.Dto


{
    [AutoMapTo(typeof(PageField))]
    public class PageFieldDto : EntityDto<long>
    {
        //public string UserName { get; set; }

        public string Name { get; set; }

        public long RequestPageId { get; set; }
        public PageFieldType Type { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public bool Nullable { get; set; }
        public string Note { get; set; }
    }
}



