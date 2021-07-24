using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using DRIMA.Configuration.Dto;

namespace DRIMA.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DRIMAAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
