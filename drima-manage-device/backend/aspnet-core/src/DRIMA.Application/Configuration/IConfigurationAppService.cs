using System.Threading.Tasks;
using DRIMA.Configuration.Dto;

namespace DRIMA.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
