using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AutoGenerateTestcase.Configuration;

namespace AutoGenerateTestcase.Web.Host.Startup
{
    [DependsOn(
       typeof(AutoGenerateTestcaseWebCoreModule))]
    public class AutoGenerateTestcaseWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AutoGenerateTestcaseWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AutoGenerateTestcaseWebHostModule).GetAssembly());
        }
    }
}
