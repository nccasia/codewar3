using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoGenerateTestcase.EntityFrameworkCore
{
    public static class AutoGenerateTestcaseDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AutoGenerateTestcaseDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AutoGenerateTestcaseDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
