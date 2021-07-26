using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using AutoGenerateTestcase.EntityFrameworkCore.Seed;

namespace AutoGenerateTestcase.EntityFrameworkCore
{
    [DependsOn(
        typeof(AutoGenerateTestcaseCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class AutoGenerateTestcaseEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<AutoGenerateTestcaseDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        AutoGenerateTestcaseDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        AutoGenerateTestcaseDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AutoGenerateTestcaseEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
