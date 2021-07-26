
using MaiAnVat.Common;
using MaiAnVat.Models;
using MaiAnVat.ServiceFramework.JobAndJobType;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.Services.JobAndJobType
{
    public class RegistrationJobService : IRegistrationJobService
    {
        private readonly MaiAnVatContext db;
        public RegistrationJobService()
        {
            db = new MaiAnVatContext();
        }
        public RegistrationJob Create(RegistrationJob model)
        {
            model.RegistrationJobK = Guid.NewGuid();
            db.RegistrationJob.Add(model);
            db.SaveChanges();
            return model;
        }

        public RegistrationJob Create()
        {
            throw new NotImplementedException();
        }

        public async Task<RegistrationJob> CreateAsync(RegistrationJob model)
        {
            model.RegistrationJobK = Guid.NewGuid();
            db.RegistrationJob.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var RegistrationJob = Read(id);
            if (RegistrationJob != null)
            {
                RegistrationJob.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var RegistrationJob = await ReadAsync(id);
            if (RegistrationJob != null)
            {
                RegistrationJob.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<RegistrationJob> Find(Expression<Func<RegistrationJob, bool>> whereExpression = null)
        {
            IQueryable<RegistrationJob> qJobs = db.RegistrationJob.Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qJobs = qJobs.Where(whereExpression);
            }
            return qJobs;
        }

        public IQueryable<RegistrationJob> Find(string searchTerm, Expression<Func<RegistrationJob, bool>> whereExpression = null)
        {
            IQueryable<RegistrationJob> qRJobs = Find(whereExpression);

            return qRJobs;
        }

        public Task<IQueryable<RegistrationJob>> FindAsync(Expression<Func<RegistrationJob, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<RegistrationJob>> FindAsync(string searchTerm, Expression<Func<RegistrationJob, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public RegistrationJob Read(Guid id)
        {
            return db.RegistrationJob.FirstOrDefault(x => x.RegistrationJobK == id);
        }

        public async Task<RegistrationJob> ReadAsync(Guid id)
        {
            return await db.RegistrationJob.FindAsync(id);
        }

        public void Update(Guid id, RegistrationJob entity)
        {
            var registrationJob = Read(id);
            if (registrationJob != null)
            {
                registrationJob.ConfirmedUserFk = entity.ConfirmedUserFk;
                registrationJob.IsAccepted = entity.IsAccepted;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, RegistrationJob entity)
        {
            var registrationJob = await ReadAsync(id);
            if (registrationJob != null)
            {
                registrationJob.ConfirmedUserFk = entity.ConfirmedUserFk;
                registrationJob.IsAccepted = entity.IsAccepted;
                await db.SaveChangesAsync();
            }
        }

        public async Task ApprovedRegistrationAsync(Guid id, RegistrationJob entity)
        {
            var registrationJob = await ReadAsync(id);
            if (registrationJob != null)
            {
                registrationJob.IsAccepted = true;
                registrationJob.ConfirmedUserFk = entity.ConfirmedUserFk;
                registrationJob.ConfirmedUtc = DateTime.Now;
                registrationJob.ModifiedByUserFk = entity.ModifiedByUserFk;
                await db.SaveChangesAsync();
            }
        }
    }
}
