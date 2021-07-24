using System.Threading.Tasks;
using Abp.Application.Services;
using DRIMA.Sessions.Dto;

namespace DRIMA.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
