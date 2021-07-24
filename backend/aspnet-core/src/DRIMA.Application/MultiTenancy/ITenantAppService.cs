using Abp.Application.Services;
using DRIMA.MultiTenancy.Dto;

namespace DRIMA.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

