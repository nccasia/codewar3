using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AutoGenerateTestcase.Controllers
{
    public abstract class AutoGenerateTestcaseControllerBase: AbpController
    {
        protected AutoGenerateTestcaseControllerBase()
        {
            LocalizationSourceName = AutoGenerateTestcaseConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
