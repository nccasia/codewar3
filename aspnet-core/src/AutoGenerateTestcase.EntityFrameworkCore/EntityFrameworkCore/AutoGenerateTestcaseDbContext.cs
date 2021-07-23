using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AutoGenerateTestcase.Authorization.Roles;
using AutoGenerateTestcase.Authorization.Users;
using AutoGenerateTestcase.MultiTenancy;

namespace AutoGenerateTestcase.EntityFrameworkCore
{
    public class AutoGenerateTestcaseDbContext : AbpZeroDbContext<Tenant, Role, User, AutoGenerateTestcaseDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public AutoGenerateTestcaseDbContext(DbContextOptions<AutoGenerateTestcaseDbContext> options)
            : base(options)
        {
        }
    }
}
