using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AutoGenerateTestcase.Authorization.Roles;
using AutoGenerateTestcase.Authorization.Users;
using AutoGenerateTestcase.MultiTenancy;
using AutoGenerateTestcase.Entities;

namespace AutoGenerateTestcase.EntityFrameworkCore
{
    public class AutoGenerateTestcaseDbContext : AbpZeroDbContext<Tenant, Role, User, AutoGenerateTestcaseDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestPage> RequestPages { get; set; }
        public DbSet<PageField> PageFields { get; set; }
        public DbSet<PageFieldCondition> PageFieldConditions { get; set; }

        public AutoGenerateTestcaseDbContext(DbContextOptions<AutoGenerateTestcaseDbContext> options)
            : base(options)
        {
        }
    }
}
