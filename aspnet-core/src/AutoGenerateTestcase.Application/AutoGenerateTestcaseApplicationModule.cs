using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AutoGenerateTestcase.Authorization;

namespace AutoGenerateTestcase
{
    [DependsOn(
        typeof(AutoGenerateTestcaseCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AutoGenerateTestcaseApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<AutoGenerateTestcaseAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AutoGenerateTestcaseApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
