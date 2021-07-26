using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DRIMA.EntityFrameworkCore;
using DRIMA.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace DRIMA.Web.Tests
{
    [DependsOn(
        typeof(DRIMAWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class DRIMAWebTestModule : AbpModule
    {
        public DRIMAWebTestModule(DRIMAEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DRIMAWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(DRIMAWebMvcModule).Assembly);
        }
    }
}