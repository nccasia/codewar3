using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AutoGenerateTestcase.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoGenerateTestcase.Constants.Enum;

namespace AutoGenerateTestcase.APIs.RequestPages.Dto
{
    [AutoMapTo(typeof(RequestPage))]
    public class RequestPageDto : EntityDto<long>
    {

        public long RequestId { get; set; }
        public string PageName { get; set; }
        public RequestPageType PageType { get; set; }
    }
}
