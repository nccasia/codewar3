using System.Threading.Tasks;
using Abp.Application.Services;
using DRIMA.Authorization.Accounts.Dto;

namespace DRIMA.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
