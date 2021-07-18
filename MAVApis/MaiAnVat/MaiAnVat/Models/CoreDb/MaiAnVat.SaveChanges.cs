using MaiAnVat.Common.Auditing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MaiAnVat.Models
{
    public partial class MaiAnVatContext : DbContext
    {
        public override int SaveChanges()
        {
            try
            {
                DateTime saveDate = DateTime.UtcNow;

                foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
                {
                    if (entry.State == EntityState.Added || entry.Entity.CreatedAtUtc == default(DateTime))
                    {
                        /*
                            * CreatedAt fields only get added when the entity state is new
                            */
                        if (entry.Entity.CreatedAtUtc == default(DateTime))
                            entry.Entity.CreatedAtUtc = saveDate;
                        if (entry.Entity.CreatedByUserFk == default(int) || entry.Entity.CreatedByUserFk == 0)
                            entry.Entity.CreatedByUserFk = 1;
                    }
                    if (entry.State != EntityState.Unchanged)
                    {
                        entry.Entity.ModifiedAtUtc = saveDate;
                        entry.Entity.ModifiedByUserFk = 1;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                DateTime saveDate = DateTime.UtcNow;

                foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
                {
                    if (entry.State == EntityState.Added || entry.Entity.CreatedAtUtc == default(DateTime))
                    {
                        /*
                            * CreatedAt fields only get added when the entity state is new
                            */
                        if (entry.Entity.CreatedAtUtc == default(DateTime))
                            entry.Entity.CreatedAtUtc = saveDate;
                        if (entry.Entity.CreatedByUserFk == default(int) || entry.Entity.CreatedByUserFk == 0)
                            entry.Entity.CreatedByUserFk = 1;
                    }
                    if (entry.State != EntityState.Unchanged)
                    {
                        entry.Entity.ModifiedAtUtc = saveDate;
                        entry.Entity.ModifiedByUserFk = 1;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
