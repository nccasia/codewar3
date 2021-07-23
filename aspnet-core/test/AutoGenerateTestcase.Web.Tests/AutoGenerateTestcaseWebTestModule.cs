using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AutoGenerateTestcase.EntityFrameworkCore;
using AutoGenerateTestcase.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AutoGenerateTestcase.Web.Tests
{
    [DependsOn(
        typeof(AutoGenerateTestcaseWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class AutoGenerateTestcaseWebTestModule : AbpModule
    {
        public AutoGenerateTestcaseWebTestModule(AutoGenerateTestcaseEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AutoGenerateTestcaseWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(AutoGenerateTestcaseWebMvcModule).Assembly);
        }
    }
}