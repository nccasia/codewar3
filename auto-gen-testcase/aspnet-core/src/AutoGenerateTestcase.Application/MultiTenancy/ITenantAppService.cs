using Abp.Application.Services;
using AutoGenerateTestcase.MultiTenancy.Dto;

namespace AutoGenerateTestcase.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

