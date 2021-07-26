using System.Threading.Tasks;
using AutoGenerateTestcase.Configuration.Dto;

namespace AutoGenerateTestcase.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
