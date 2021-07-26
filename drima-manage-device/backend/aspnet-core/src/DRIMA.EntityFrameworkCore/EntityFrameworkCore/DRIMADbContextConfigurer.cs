using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DRIMA.EntityFrameworkCore
{
    public static class DRIMADbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DRIMADbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DRIMADbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
