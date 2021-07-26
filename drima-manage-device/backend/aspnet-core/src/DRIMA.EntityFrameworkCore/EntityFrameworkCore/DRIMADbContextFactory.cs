using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DRIMA.Configuration;
using DRIMA.Web;

namespace DRIMA.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DRIMADbContextFactory : IDesignTimeDbContextFactory<DRIMADbContext>
    {
        public DRIMADbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DRIMADbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DRIMADbContextConfigurer.Configure(builder, configuration.GetConnectionString(DRIMAConsts.ConnectionStringName));

            return new DRIMADbContext(builder.Options);
        }
    }
}
