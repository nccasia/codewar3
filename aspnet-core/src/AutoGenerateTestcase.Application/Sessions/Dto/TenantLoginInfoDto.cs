using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AutoGenerateTestcase.MultiTenancy;

namespace AutoGenerateTestcase.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
