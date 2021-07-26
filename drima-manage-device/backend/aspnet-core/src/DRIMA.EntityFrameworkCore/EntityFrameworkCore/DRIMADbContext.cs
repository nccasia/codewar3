using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using DRIMA.Authorization.Roles;
using DRIMA.Authorization.Users;
using DRIMA.MultiTenancy;
using DRIMA.Entities;

namespace DRIMA.EntityFrameworkCore
{
    public class DRIMADbContext : AbpZeroDbContext<Tenant, Role, User, DRIMADbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Device> Devices { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<DeviceImage> DeviceImages { get; set; }

        public DRIMADbContext(DbContextOptions<DRIMADbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().Property(i => i.Id).ValueGeneratedNever();
            modelBuilder.Entity<Request>().Property(i => i.Id).ValueGeneratedNever();
            modelBuilder.Entity<DeviceImage>().Property(i => i.Id).ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }
    }
}
