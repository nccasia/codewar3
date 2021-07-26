
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AutoGenerateTestcase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static AutoGenerateTestcase.Constants.Enum;

namespace AutoGenerateTestcase.APIs.Requests.Dto
{
    [AutoMapTo(typeof(Request))]
    public class RequestDto : EntityDto<long>
    {
        //public string UserName { get; set; }
        public RequestStatus Status { get; set; }

        public string Name { get; set; }
        public DateTime Deadline { get; set; }
    }
}
