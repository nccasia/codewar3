
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
    public class JobService : IJobService
    {
        private readonly MaiAnVatContext db;
        public JobService()
        {
            db = new MaiAnVatContext();
        }
        public Job Create(Job model)
        {
            model.JobK = Guid.NewGuid();
            db.Job.Add(model);
            db.SaveChanges();
            return model;
        }

        public Job Create()
        {
            throw new NotImplementedException();
        }

        public async Task<Job> CreateAsync(Job model)
        {
            model.JobK = Guid.NewGuid();
            db.Job.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var Job = Read(id);
            if (Job != null)
            {
                Job.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var Job = await ReadAsync(id);
            if (Job != null)
            {
                Job.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<Job> Find(Expression<Func<Job, bool>> whereExpression = null)
        {
            IQueryable<Job> qJobs = db.Job.Include(x => x.JobAssignedUser).Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qJobs = qJobs.Where(whereExpression);
            }
            return qJobs;
        }

        public IQueryable<Job> Find(string searchTerm, Expression<Func<Job, bool>> whereExpression = null)
        {
            IQueryable<Job> qJobs = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qJobs = qJobs.Where(i => i.Name.Contains(searchTerm) || i.Description.Contains(searchTerm));
            }
            return qJobs;
        }

        public Task<IQueryable<Job>> FindAsync(Expression<Func<Job, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<Job>> FindAsync(string searchTerm, Expression<Func<Job, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public Job Read(Guid id)
        {
            return db.Job.FirstOrDefault(x => x.JobK == id);
        }

        public async Task<Job> ReadAsync(Guid id)
        {
            return await db.Job.FindAsync(id);
        }

        public async Task<Job> SubmitAsync(Job entity)
        {
            var job = Read(entity.JobK);
            if (job != null)
            {
                job.IsSubmitted = entity.IsSubmitted;
                job.ModifiedByUserFk = entity.ModifiedByUserFk;
                await db.SaveChangesAsync();
            }
            return job;
        }

        public void Update(Guid id, Job entity)
        {
            var Job = Read(id);
            if (Job != null)
            {
                Job.Title = entity.Title;
                Job.JobTypeFk = entity.JobTypeFk;
                Job.WorkflowStatusFk = entity.WorkflowStatusFk;
                Job.Name = entity.Name;
                Job.Description = entity.Description;
                Job.RegistrationDeadline = entity.RegistrationDeadline;
                Job.RegistrationJob = entity.RegistrationJob;
                Job.CustomerOrder = entity.CustomerOrder;
                Job.ModifiedByUserFk = entity.ModifiedByUserFk;
                Job.IsSubmitted = entity.IsSubmitted;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, Job entity)
        {
            var Job = await ReadAsync(id);
            if (Job != null)
            {
                Job.Title = entity.Title;
                Job.JobTypeFk = entity.JobTypeFk;
                Job.WorkflowStatusFk = entity.WorkflowStatusFk;
                Job.Name = entity.Name;
                Job.Description = entity.Description;
                Job.RegistrationDeadline = entity.RegistrationDeadline;
                Job.RegistrationJob = entity.RegistrationJob;
                Job.CustomerOrder = entity.CustomerOrder;
                Job.ModifiedByUserFk = entity.ModifiedByUserFk;
                Job.IsSubmitted = entity.IsSubmitted;
                await db.SaveChangesAsync();
            }
        }
    }
}
