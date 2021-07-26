using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DRIMA.Authorization;

namespace DRIMA
{
    [DependsOn(
        typeof(DRIMACoreModule), 
        typeof(AbpAutoMapperModule))]
    public class DRIMAApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<DRIMAAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DRIMAApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
