using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DRIMA.Configuration;

namespace DRIMA.Web.Host.Startup
{
    [DependsOn(
       typeof(DRIMAWebCoreModule))]
    public class DRIMAWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DRIMAWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DRIMAWebHostModule).GetAssembly());
        }
    }
}
