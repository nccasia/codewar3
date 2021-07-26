using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AutoGenerateTestcase.Configuration;
using AutoGenerateTestcase.EntityFrameworkCore;
using AutoGenerateTestcase.Migrator.DependencyInjection;

namespace AutoGenerateTestcase.Migrator
{
    [DependsOn(typeof(AutoGenerateTestcaseEntityFrameworkModule))]
    public class AutoGenerateTestcaseMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AutoGenerateTestcaseMigratorModule(AutoGenerateTestcaseEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(AutoGenerateTestcaseMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                AutoGenerateTestcaseConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AutoGenerateTestcaseMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
