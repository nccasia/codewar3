using System.Threading.Tasks;
using Abp.Application.Services;
using AutoGenerateTestcase.Authorization.Accounts.Dto;

namespace AutoGenerateTestcase.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
