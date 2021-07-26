using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace DRIMA.Controllers
{
    public abstract class DRIMAControllerBase: AbpController
    {
        protected DRIMAControllerBase()
        {
            LocalizationSourceName = DRIMAConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
