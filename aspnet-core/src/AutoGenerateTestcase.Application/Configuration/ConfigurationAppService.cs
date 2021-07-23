using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using AutoGenerateTestcase.Configuration.Dto;

namespace AutoGenerateTestcase.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AutoGenerateTestcaseAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
