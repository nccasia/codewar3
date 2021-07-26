using System.Threading.Tasks;
using Abp.Application.Services;
using AutoGenerateTestcase.Sessions.Dto;

namespace AutoGenerateTestcase.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
