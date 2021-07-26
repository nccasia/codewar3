using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using AutoGenerateTestcase.Configuration;
using AutoGenerateTestcase.Web;

namespace AutoGenerateTestcase.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AutoGenerateTestcaseDbContextFactory : IDesignTimeDbContextFactory<AutoGenerateTestcaseDbContext>
    {
        public AutoGenerateTestcaseDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AutoGenerateTestcaseDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AutoGenerateTestcaseDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AutoGenerateTestcaseConsts.ConnectionStringName));

            return new AutoGenerateTestcaseDbContext(builder.Options);
        }
    }
}
